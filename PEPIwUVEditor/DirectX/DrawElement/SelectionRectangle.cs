using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class SelectionRectangle : IDrawElement
    {
        private bool disposedValue;
        private Color4 color;

        Device Device { get; }
        EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        InputLayout VertexLayout { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }

        public Color4 Color
        {
            get => color;
            set
            {
                color = value;
                CreateVertexBuffer();
            }
        }

        public Vector2 StartPos { get; set; }
        public Vector2 EndPos { get; set; }

        public SelectionRectangle(Device device, Effect effect, RasterizerState drawMode, Color4 color)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass");
            DrawMode = drawMode;
            Color = color;

            CreateVertexLayout();
            CreateIndexBuffer();
        }

        public void Prepare()
        {
            // 描画方式を設定
            UsingEffectPass.Apply(Device.ImmediateContext);
            Device.ImmediateContext.Rasterizer.State = DrawMode;
            Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // レイアウトを設定
            Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;

            // バッファを設定
            CreateVertexBuffer();
            Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexStruct.SizeInBytes, 0)
            );
            Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);

            Device.ImmediateContext.DrawIndexed(3, 0, 0);
            Device.ImmediateContext.DrawIndexed(3, 3, 0);
        }

        void CreateVertexLayout()
        {
            VertexLayout?.Dispose();
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                VertexStruct.VertexElements
            );
        }

        void CreateVertexBuffer()
        {
            VertexBuffer?.Dispose();
            using (var vertexStream = new DataStream(CreateVertices(), true, true))
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

        void CreateIndexBuffer()
        {
            IndexBuffer?.Dispose();
            using (DataStream indexStream = new DataStream(CreateIndices(), true, true))
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

        private VertexStruct[] CreateVertices()
        {
            Vector2 min = new Vector2(Math.Min(StartPos.X, EndPos.X), Math.Min(StartPos.Y, EndPos.Y));
            Vector2 max = new Vector2(Math.Max(StartPos.X, EndPos.X), Math.Max(StartPos.Y, EndPos.Y));

            return new[]
            {
                new VertexStruct
                {
                    Position = new Vector3(min.X, max.Y, 0),
                    Color = Color,
                    TEXCOORD = new Vector2(0, 0)
                },
                new VertexStruct
                {
                    Position = new Vector3(max.X, max.Y, 0),
                    Color = Color,
                    TEXCOORD = new Vector2(1, 0)
                },
                new VertexStruct
                {
                    Position = new Vector3(min.X, min.Y, 0),
                    Color = Color,
                    TEXCOORD = new Vector2(0 ,1)
                },
                new VertexStruct
                {
                    Position = new Vector3(max.X, min.Y, 0),
                    Color = Color,
                    TEXCOORD = new Vector2(1, 1)
                },
            };
        }

        private uint[] CreateIndices() => new uint[] {
            0, 1, 2,
            3, 2, 1
        };


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
        // ~SelectionRectangle()
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
