using DxManager;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.RawInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Buffer = SlimDX.Direct3D11.Buffer;

namespace IwUVEditor
{
    class UVViewDrawProcess : DxProcess
    {
        private Material currentMaterial;
        private int texPlateVertexCount;
        private int texPlateindexCount;

        private bool isViewMoving = false;

        InputLayout VertexLayoutOfTexturePlates { get; set; }
        InputLayout VertexLayoutOfUVMesh { get; set; }
        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }
        Buffer TexturePlateInstancesBuffer { get; set; }

        EffectPass DrawTexturePlatePass { get; set; }
        EffectPass DrawMeshVertexPass { get; set; }

        public Vector3 ShiftOffset { get; set; } = Vector3.Zero; //publicなのはデバッグ用
        public ScaleManager Scale { get; } = new ScaleManager() //publicなのはデバッグ用
        {
            DeltaOffset = -8000,
            Amplitude = 1000,
            Offset = 0,
            Step = 0.1f,
            Gain = 1,
        };

        public float PeripheryPlateAlpha { get; set; } = 0.5f;
        public int Radius { get; set; } = 5;

        public bool IsActive { get; set; } = true;
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>()
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };

        Dictionary<Material, ShaderResourceView> TextureCache { get; } = new Dictionary<Material, ShaderResourceView>();
        Dictionary<Material, VertexStruct[]> UVMeshCache { get; } = new Dictionary<Material, VertexStruct[]>();

        ShaderResourceView Texture { get; set; }
        List<InstanceOffset> TexturePlateInstances;
        VertexStruct[] UVMesh { get; set; }
        uint[] UVMeshIndices { get; set; }

        RasterizerStateProvider Rasterize { get; set; }

        public Material CurrentMaterial
        {
            get => currentMaterial;
            set
            {
                if (Context is null)
                    return;
                currentMaterial = value;
                if (!TextureCache.Keys.Contains(value))
                {
                    TextureCache.Add(value, LoadTexture(value));
                    UVMeshCache.Add(value, LoadUVVertices(value));
                }

                Texture = TextureCache[value];
                UVMesh = UVMeshCache[value];
                UVMeshIndices = value.FaceSequence;

                CreateVertexBuffer();
                CreateIndexBuffer();
            }
        }

        public override void Init()
        {
            DrawTexturePlatePass = Effect.GetTechniqueByName("TexturePlatesTechnique").GetPassByName("DrawTexturePlatesPass");
            DrawMeshVertexPass = Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass");

            // 頂点情報のフォーマットを設定
            VertexLayoutOfTexturePlates = new InputLayout(
                Context.Device,
                DrawTexturePlatePass.Description.Signature,
                VertexStruct.VertexElements.Concat(InstanceOffset.VertexElements).ToArray()
            );

            VertexLayoutOfUVMesh = new InputLayout(
                Context.Device,
                DrawMeshVertexPass.Description.Signature,
                VertexStruct.VertexElements
            );

            // 頂点バッファに頂点を追加
            CreateVertexBuffer();

            // インデックスバッファを作成
            CreateIndexBuffer();

            // テクスチャ板のインスタンスを作成
            CreateTexturePlateInstancesBuffer();

            Texture = LoadTexture(null);

            Rasterize = new RasterizerStateProvider(Context.Device);
        }

        public override void Draw()
        {
            // 背景を灰色に
            Context.Device.ImmediateContext.ClearRenderTargetView(Context.RenderTarget, new Color4(1.0f, 0.3f, 0.3f, 0.3f));
            // 深度バッファ
            Context.Device.ImmediateContext.ClearDepthStencilView(Context.DepthStencil, DepthStencilClearFlags.Depth, 1, 0);
            // テクスチャを読み込み
            Effect.GetVariableByName("diffuseTexture").AsResource().SetResource(Texture);

            // 三角形をデバイスに入力
            Context.Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexStruct.SizeInBytes, 0),
                new VertexBufferBinding(TexturePlateInstancesBuffer, InstanceOffset.SizeInBytes, 0)
            );
            Context.Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            Context.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // テクスチャ板を描画
            Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayoutOfTexturePlates;
            DrawTexturePlatePass.Apply(Context.Device.ImmediateContext);
            Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Solid;
            Context.Device.ImmediateContext.DrawIndexedInstanced(3, TexturePlateInstances.Count, 0, 0, 0);
            Context.Device.ImmediateContext.DrawIndexedInstanced(3, TexturePlateInstances.Count, 3, 0, 0);

            // UVメッシュを描画
            if (!(CurrentMaterial is null))
            {
                Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayoutOfUVMesh;
                DrawMeshVertexPass.Apply(Context.Device.ImmediateContext);
                Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Wireframe;
                for (int i = 0; i < CurrentMaterial.Faces.Count; i++)
                {
                    Context.Device.ImmediateContext.DrawIndexed(3, texPlateindexCount + i * 3, texPlateVertexCount);
                }
            }

            // 描画内容を反映
            Context.SwapChain.Present(0, PresentFlags.None);
        }

        protected override void UpdateCamera()
        {
            var scale = Scale.Scale;
            Matrix transMatrix = Camera.GetMatrix() * Matrix.Translation(ShiftOffset) * Matrix.Scaling(scale, scale, 1);
            Effect.GetVariableByName("ViewProjection").AsMatrix().SetMatrix(transMatrix);
        }
        public void ResetCamera()
        {
            ShiftOffset = Vector3.Zero;
            Scale.WheelDelta = 0;
        }

        protected override void MouseInput(object sender, MouseInputEventArgs e)
        {
            if (!IsActive)
                return;

            bool isViewScaling = false;

            switch (e.ButtonFlags)
            {
                case MouseButtonFlags.None:
                    break;
                case MouseButtonFlags.MouseWheel:
                    isViewScaling = true;
                    break;
                case MouseButtonFlags.Button5Up:
                    break;
                case MouseButtonFlags.Button5Down:
                    break;
                case MouseButtonFlags.Button4Up:
                    break;
                case MouseButtonFlags.Button4Down:
                    break;
                case MouseButtonFlags.MiddleUp:
                    isViewMoving = false;
                    break;
                case MouseButtonFlags.MiddleDown:
                    isViewMoving = true;
                    break;
                case MouseButtonFlags.RightUp:
                    break;
                case MouseButtonFlags.RightDown:
                    break;
                case MouseButtonFlags.LeftUp:
                    break;
                case MouseButtonFlags.LeftDown:
                    break;
                default:
                    break;
            }

            float modifier = (IsPress[Keys.ShiftKey] ? 4f : 1f) / (IsPress[Keys.ControlKey] ? 4f : 1f);
            if (isViewMoving)
                ShiftOffset += modifier * new Vector3(1f * e.X / Context.TargetControl.Width, -1f * e.Y / Context.TargetControl.Height, 0) / Scale.Scale;
            if (isViewScaling)
                Scale.WheelDelta += e.WheelDelta * modifier;
        }

        private void CreateVertexBuffer()
        {
            using (var vertexStream = new DataStream(CreateVertices(), true, true))
            {
                VertexBuffer = new Buffer(
                    Context.Device,
                    vertexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)vertexStream.Length,
                        BindFlags = BindFlags.VertexBuffer,
                    }
                );
            }
        }

        private void CreateIndexBuffer()
        {
            using (DataStream indexStream = new DataStream(CreateIndices(), true, true)
            )
            {
                IndexBuffer = new Buffer(
                    Context.Device,
                    indexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)indexStream.Length,
                        BindFlags = BindFlags.IndexBuffer,
                    }
                );
            }
        }

        private void CreateTexturePlateInstancesBuffer()
        {
            CreateTexPlateInstances();

            TexturePlateInstancesBuffer = new Buffer(
                Context.Device,
                new DataStream(TexturePlateInstances.ToArray(), false, true),
                new BufferDescription
                (
                    InstanceOffset.SizeInBytes * TexturePlateInstances.Count,
                    ResourceUsage.Dynamic,
                    BindFlags.VertexBuffer,
                    CpuAccessFlags.Write,
                    ResourceOptionFlags.None,
                    0
                )
            );
        }

        private VertexStruct[] CreateVertices()
        {
            VertexStruct[] texturePlate = new[] {
                    new VertexStruct
                    {
                        Position = new Vector3(-1, 1, 0),
                        Color = new Color4(1, 1, 1, 1),
                        TEXCOORD = new Vector2(0, 0)
                    },
                    new VertexStruct
                    {
                        Position = new Vector3(1, 1, 0),
                        Color = new Color4(1, 1, 1, 1),
                        TEXCOORD = new Vector2(1, 0)
                    },
                    new VertexStruct
                    {
                        Position = new Vector3(-1, -1, 0),
                        Color = new Color4(1, 1, 1, 1),
                        TEXCOORD = new Vector2(0 ,1)
                    },
                    new VertexStruct
                    {
                        Position = new Vector3(1, -1, 0),
                        Color = new Color4(1, 1, 1, 1),
                        TEXCOORD = new Vector2(1, 1)
                    }
                };
            texPlateVertexCount = texturePlate.Length;

            return UVMesh is null ? texturePlate : texturePlate.Concat(UVMesh).ToArray();
        }

        private uint[] CreateIndices()
        {
            uint[] texturePlate = new uint[] {
                    0, 1, 2,
                    3, 2, 1
                };
            texPlateindexCount = texturePlate.Length;

            return UVMeshIndices is null ? texturePlate : texturePlate.Concat(UVMeshIndices).ToArray();
        }

        private void CreateTexPlateInstances()
        {
            TexturePlateInstances = new List<InstanceOffset>();
            // 半径nで四角形を放射配置するため、{0..n}と{0..n}の直積集合でループする
            foreach ((int i, int j) in Enumerable.Range(0, Radius + 1).SelectMany(i => Enumerable.Range(0, Radius + 1).Select(j => (i, j))))
            {
                var x = i * 2;
                var y = j * 2;

                TexturePlateInstances.Add(
                    new InstanceOffset()
                    {
                        Offset = Matrix.Translation(x, y, 0),
                        AlphaRatio = (i == 0 && j == 0) ? 1 : PeripheryPlateAlpha
                    }
                    );

                if (y != 0)
                    TexturePlateInstances.Add(
                        new InstanceOffset()
                        {
                            Offset = Matrix.Translation(x, -y, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                        );
                if (x != 0)
                    TexturePlateInstances.Add(
                    new InstanceOffset()
                    {
                        Offset = Matrix.Translation(-x, y, 0),
                        AlphaRatio = PeripheryPlateAlpha
                    }
                    );
                if (i != 0 && j != 0)
                    TexturePlateInstances.Add(
                    new InstanceOffset()
                    {
                        Offset = Matrix.Translation(-x, -y, 0),
                        AlphaRatio = PeripheryPlateAlpha
                    }
                    );
            }
        }

        private Texture2D TextureFromBitmap(Bitmap bitmap)
        {
            using (LockedBitmap lockedBitmap = new LockedBitmap(bitmap))
            using (DataStream dataStream = new DataStream(lockedBitmap.BitmapData.Scan0, Math.Abs(lockedBitmap.BitmapData.Stride) * lockedBitmap.BitmapData.Height, true, false))
                return new Texture2D(
                    Context.Device,
                    new Texture2DDescription
                    {
                        BindFlags = BindFlags.ShaderResource,
                        CpuAccessFlags = CpuAccessFlags.None,
                        Format = Format.B8G8R8A8_UNorm,
                        OptionFlags = ResourceOptionFlags.None,
                        MipLevels = 1,
                        Usage = ResourceUsage.Immutable,
                        Width = bitmap.Width,
                        Height = bitmap.Height,
                        ArraySize = 1,
                        SampleDescription = new SampleDescription(1, 0)
                    },
                    new DataRectangle(lockedBitmap.BitmapData.Stride, dataStream)
                );
        }

        private ShaderResourceView LoadTexture(Material material)
        {
            if ((material is null) || string.IsNullOrWhiteSpace(material.Tex))
                return new ShaderResourceView(Context.Device, TextureFromBitmap(Properties.Resources.White));

            if (material.TexExt.ToLower() == ".tga")
            {
                using (var tgaMap = new TGASharpLib.TGA(material.TexFullPath).ToBitmap())
                using (var tex = new Bitmap(tgaMap))
                {
                    return new ShaderResourceView(Context.Device, TextureFromBitmap(tex));
                }
            }

            return ShaderResourceView.FromFile(Context.Device, material.TexFullPath);
        }

        private VertexStruct[] LoadUVVertices(Material material) =>
            material.Vertices.Select(vtx => new VertexStruct()
            {
                Position = new Vector3(new Vector2(vtx.UV.X * 2 - 1, 1 - vtx.UV.Y * 2), 0),
                Color = new Color4(1, 0, 0, 0),
                TEXCOORD = vtx.UV
            }
            ).ToArray();

        protected override void Dispose(bool disposing)
        {
            VertexLayoutOfUVMesh?.Dispose();
            VertexLayoutOfTexturePlates?.Dispose();
            VertexBuffer?.Dispose();
            TexturePlateInstancesBuffer?.Dispose();
            Texture = null;
            foreach (var resource in TextureCache.Values)
            {
                resource?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
