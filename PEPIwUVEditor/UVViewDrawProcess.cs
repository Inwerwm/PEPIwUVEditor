using DxManager;
using PEPlugin.Pmx;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class UVViewDrawProcess : DxProcess
    {
        public IEnumerable<Material> Materials { get; set; }

        InputLayout VertexLayout { get; set; }
        SlimDX.Direct3D11.Buffer VertexBuffer { get; set; }
        SlimDX.Direct3D11.Buffer IndexBuffer { get; set; }

        RasterizerStateProvider Rasterize { get; set; }

        public UVViewDrawProcess(IEnumerable<Material> materials)
        {
            Materials = materials;
        }

        public override void Init()
        {
            // 頂点情報のフォーマットを設定
            VertexLayout = new InputLayout(
                Context.Device,
                Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Description.Signature,
                VertexPositionColor.VertexElements
            );

            // 頂点バッファに頂点を追加
            using (var vertexStream = new DataStream(
                new[] {
                    new VertexPositionColor
                    {
                        Position = new Vector3(-1, 1, 0),
                        Color = new Vector3(1, 1, 1)
                    },
                    new VertexPositionColor
                    {
                        Position = new Vector3(1, 1, 0),
                        Color = new Vector3(1, 1, 1)
                    },
                    new VertexPositionColor
                    {
                        Position = new Vector3(-1, -1, 0),
                        Color = new Vector3(1, 1, 1)
                    },
                    new VertexPositionColor
                    {
                        Position = new Vector3(1, -1, 0),
                        Color = new Vector3(1, 1, 1)
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
            using (SlimDX.DataStream indexStream = new SlimDX.DataStream(
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

            // 三角形をデバイスに入力
            Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;
            Context.Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexPositionColor.SizeInBytes, 0)
            );
            Context.Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            Context.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // 三角形を描画
            Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(Context.Device.ImmediateContext);
            Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Solid;
            Context.Device.ImmediateContext.DrawIndexed(6, 0, 0);

            // ワイヤーフレーム描画
            //Context.Device.ImmediateContext.Rasterizer.State = Rasterize.Wireframe;
            //Context.Device.ImmediateContext.Draw(3, 3);

            // 描画内容を反映
            Context.SwapChain.Present(0, PresentFlags.None);
        }

        protected override void Dispose(bool disposing)
        {
            VertexLayout?.Dispose();
            VertexBuffer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
