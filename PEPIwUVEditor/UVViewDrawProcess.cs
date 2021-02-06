using DxManager;
using PEPlugin.Pmx;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class UVViewDrawProcess : DxProcess
    {
        private Material currentMaterial;

        InputLayout VertexLayout { get; set; }
        SlimDX.Direct3D11.Buffer VertexBuffer { get; set; }
        SlimDX.Direct3D11.Buffer IndexBuffer { get; set; }
        ShaderResourceView Texture { get; set; }

        RasterizerStateProvider Rasterize { get; set; }

        Dictionary<Material, ShaderResourceView> Cache { get; } = new Dictionary<Material, ShaderResourceView>();
        public Material Material
        {
            get => currentMaterial;
            set
            {
                if (Context is null)
                    return;
                currentMaterial = value;
                if (!Cache.Keys.Contains(value))
                    Cache.Add(value, LoadTexture(currentMaterial));
                Texture = Cache[value];
            }
        }

        public override void Init()
        {
            // 頂点情報のフォーマットを設定
            VertexLayout = new InputLayout(
                Context.Device,
                Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawTexturePass").Description.Signature,
                VertexStruct.VertexElements
            );

            // 頂点バッファに頂点を追加
            using (var vertexStream = new DataStream(
                new[] {
                    new VertexStruct
                    {
                        Position = new Vector3(-1, 1, 0),
                        Color = new Vector3(1, 1, 1),
                        TEXCOORD = new Vector2(0, 0)
                    },
                    new VertexStruct
                    {
                        Position = new Vector3(1, 1, 0),
                        Color = new Vector3(1, 1, 1),
                        TEXCOORD = new Vector2(1, 0)
                    },
                    new VertexStruct
                    {
                        Position = new Vector3(-1, -1, 0),
                        Color = new Vector3(1, 1, 1),
                        TEXCOORD = new Vector2(0 ,1)
                    },
                    new VertexStruct
                    {
                        Position = new Vector3(1, -1, 0),
                        Color = new Vector3(1, 1, 1),
                        TEXCOORD = new Vector2(1, 1)
                    }
                },
                true,
                true
            ))
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

            // インデックスバッファを作成
            using (DataStream indexStream = new DataStream(
                new uint[] {
                    0, 1, 2,
                    1, 2, 3
                },
                true,
                true
                )
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

            Texture = LoadTexture(null);

            Rasterize = new RasterizerStateProvider(Context.Device);
        }
        public override void Draw()
        {
            // 背景を灰色に
            Context.Device.ImmediateContext.ClearRenderTargetView(Context.RenderTarget, new Color4(1.0f, 0.3f, 0.3f, 0.3f));
            // 深度バッファ
            Context.Device.ImmediateContext.ClearDepthStencilView(
            Context.DepthStencil,
            DepthStencilClearFlags.Depth,
            1,
            0
            );
            // テクスチャを読み込み
            Effect.GetVariableByName("diffuseTexture").AsResource().SetResource(Texture);

            // 三角形をデバイスに入力
            Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;
            Context.Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexStruct.SizeInBytes, 0)
            );
            Context.Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            Context.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // 三角形を描画
            //Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass").Apply(Context.Device.ImmediateContext);
            Effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawTexturePass").Apply(Context.Device.ImmediateContext);
            Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Solid;
            Context.Device.ImmediateContext.DrawIndexed(6, 0, 0);

            // ワイヤーフレーム描画
            //Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Wireframe;
            //Context.Device.ImmediateContext.Draw(3, 3);

            // 描画内容を反映
            Context.SwapChain.Present(0, PresentFlags.None);
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

        private ShaderResourceView LoadTexture(Material material) =>
                   (material is null) || string.IsNullOrWhiteSpace(material.Tex)
                 ? new ShaderResourceView(Context.Device, TextureFromBitmap(Properties.Resources.White))
                 : ShaderResourceView.FromFile(Context.Device, material.TexFullPath);

        protected override void Dispose(bool disposing)
        {
            VertexLayout?.Dispose();
            VertexBuffer?.Dispose();
            Texture = null;
            foreach (var resource in Cache.Values)
            {
                resource?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
