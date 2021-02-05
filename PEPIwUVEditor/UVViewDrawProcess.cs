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
    public class UVViewDrawProcess : DxProcess
    {
        public IPXPmx Pmx { get; set; }

        InputLayout VertexLayout { get; set; }
        SlimDX.Direct3D11.Buffer VertexBuffer { get; set; }

        public UVViewDrawProcess(IPXPmx pmx)
        {
            Pmx = pmx;
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
                        Position = new Vector3(0, 0, 0),
                        Color = new Vector3(1, 1, 1)
                    },
                    new VertexPositionColor
                    {
                        Position = new Vector3(0, 1, 0),
                        Color = new Vector3(0, 0, 1)
                    },
                    new VertexPositionColor
                    {
                        Position = new Vector3(1, 0, 0),
                        Color = new Vector3(1, 0, 0)
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
        }
        public override void Draw()
        {
            // 背景を灰色に
            Context.Device.ImmediateContext.ClearRenderTargetView(Context.RenderTarget, new Color4(1.0f, 0.3f, 0.3f, 0.3f));
            // 深度バッファ
            //Context.Device.ImmediateContext.ClearDepthStencilView()

            // 三角形をデバイスに入力
            Context.Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;
            Context.Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexPositionColor.SizeInBytes, 0)
            );
            Context.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            // 三角形を描画
            Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(Context.Device.ImmediateContext);
            Context.Device.ImmediateContext.Draw(3, 0);

            // 描画内容を反映
            Context.SwapChain.Present(0, SlimDX.DXGI.PresentFlags.None);
        }

        protected override void Dispose(bool disposing)
        {
            VertexLayout?.Dispose();
            VertexBuffer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
