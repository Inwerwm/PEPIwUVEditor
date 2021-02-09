using DxManager;
using PEPlugin.Pmx;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.RawInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor
{
    class UVViewDrawProcess : DxProcess
    {
        private Material currentMaterial;
        private int texPlateVertexCount;
        private int texPlateindexCount;

        private bool isViewMoving = false;

        InputLayout VertexLayoutOfTexPlate { get; set; }
        InputLayout VertexLayoutOfUVMesh { get; set; }
        SlimDX.Direct3D11.Buffer VertexBuffer { get; set; }
        SlimDX.Direct3D11.Buffer IndexBuffer { get; set; }

        public Vector3 ShiftOffset { get; set; } = Vector3.Zero; //publicなのはデバッグ用
        public ScaleManager Scale { get; } = new ScaleManager() //publicなのはデバッグ用
        {
            DeltaOffset = -8000,
            Amplitude = 1000,
            Offset = 0,
            Step = 0.1f,
            Gain = 1,
        };

        public bool IsActive { get; set; } = true;
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>()
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };

        Dictionary<Material, ShaderResourceView> TextureCache { get; } = new Dictionary<Material, ShaderResourceView>();
        Dictionary<Material, VertexStruct[]> UVMeshCache { get; } = new Dictionary<Material, VertexStruct[]>();

        ShaderResourceView Texture { get; set; }
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
                    UVMeshCache.Add(value, LoadVertices(value));
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
            // 頂点情報のフォーマットを設定
            VertexLayoutOfTexPlate = new InputLayout(
                Context.Device,
                Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawTexturePass").Description.Signature,
                VertexStruct.VertexElements
            );
            VertexLayoutOfUVMesh = new InputLayout(
                Context.Device,
                Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass").Description.Signature,
                VertexStruct.VertexElements
            );

            // 頂点バッファに頂点を追加
            CreateVertexBuffer();

            // インデックスバッファを作成
            CreateIndexBuffer();

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
                new VertexBufferBinding(VertexBuffer, VertexStruct.SizeInBytes, 0)
            );
            Context.Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            Context.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // テクスチャ板を描画
            Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayoutOfTexPlate;
            Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawTexturePass").Apply(Context.Device.ImmediateContext);
            Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Solid;
            Context.Device.ImmediateContext.DrawIndexed(3, 0, 0);
            Context.Device.ImmediateContext.DrawIndexed(3, 3, 0);

            // UVメッシュを描画
            if (!(CurrentMaterial is null))
            {
                Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayoutOfUVMesh;
                Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass").Apply(Context.Device.ImmediateContext);
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
                VertexBuffer = new SlimDX.Direct3D11.Buffer(
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
                IndexBuffer = new SlimDX.Direct3D11.Buffer(
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

        private uint[] CreateIndices()
        {
            uint[] texturePlate = new uint[] {
                    0, 1, 2,
                    3, 2, 1 
                };
            texPlateindexCount = texturePlate.Length;

            return UVMeshIndices is null ? texturePlate : texturePlate.Concat(UVMeshIndices).ToArray();
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

        private VertexStruct[] LoadVertices(Material material) =>
            material.Vertices.Select(vtx => new VertexStruct()
            {
                Position = new Vector3(new Vector2(vtx.UV.X * 2 - 1, 1 - vtx.UV.Y * 2), 0),
                Color = new Color4(1, 0, 0, 0),
                TEXCOORD = vtx.UV
            }
            ).ToArray();

        protected override void Dispose(bool disposing)
        {
            VertexLayoutOfTexPlate?.Dispose();
            VertexBuffer?.Dispose();
            Texture = null;
            foreach (var resource in TextureCache.Values)
            {
                resource?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
