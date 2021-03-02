using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    abstract class DrawElement<TVertex> : IDrawElement where TVertex : IDxVertex, new()
    {
        protected bool disposedValue;

        protected Device Device { get; }
        protected EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        protected InputLayout VertexLayout { get; set; }

        protected Buffer VertexBuffer { get; set; }
        protected Buffer IndexBuffer { get; set; }

        protected DrawElement(Device device, EffectPass usingEffectPass, RasterizerState drawMode)
        {
            Device = device;
            UsingEffectPass = usingEffectPass;
            DrawMode = drawMode;
        }

        protected void Initialize()
        {
            CreateVertexLayout();
            CreateVertexBuffer();
            CreateIndexBuffer();
        }

        public virtual void UpdateVertices()
        {
            CreateVertexBuffer();
        }

        public virtual void Prepare()
        {
            // 描画方式を設定
            UsingEffectPass.Apply(Device.ImmediateContext);
            Device.ImmediateContext.Rasterizer.State = DrawMode;
            Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // レイアウトを設定
            Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;

            // バッファを設定
            Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, new TVertex().SizeInBytes, 0)
            );
            Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);

            DrawToDevice();
        }

        /// <summary>
        /// Prepare()から呼ばれるポリゴンの描画方法
        /// </summary>
        protected abstract void DrawToDevice();

        protected abstract TVertex[] CreateVertices();
        protected abstract uint[] CreateIndices();

        protected virtual void CreateVertexLayout()
        {
            VertexLayout?.Dispose();
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                new TVertex().VertexElements
            );
        }

        protected virtual void CreateVertexBuffer()
        {
            VertexBuffer?.Dispose();
            using (var vertexStream = new DataStream(CreateVertices(), true, true))
            {
                VertexBuffer = new Buffer(
                    Device,
                    vertexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)vertexStream.Length,
                        BindFlags = BindFlags.VertexBuffer,
                    }
                );
            }
        }

        protected virtual void CreateIndexBuffer()
        {
            IndexBuffer?.Dispose();
            using (DataStream indexStream = new DataStream(CreateIndices(), true, true))
            {
                IndexBuffer = new Buffer(
                    Device,
                    indexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)indexStream.Length,
                        BindFlags = BindFlags.IndexBuffer,
                    }
                );
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    VertexLayout?.Dispose();
                    VertexBuffer?.Dispose();
                    IndexBuffer?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~DrawElement()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
