using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Linq;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;


namespace IwUVEditor.DirectX.DrawElement
{
    class UVMesh : IDrawElement
    {
        private bool disposedValue;
        private Color4 lineColor;

        Device Device { get; }
        EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        InputLayout VertexLayout { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }

        Material SourceMaterial { get; }

        public Color4 LineColor
        {
            get => lineColor;
            set
            {
                lineColor = value;
                UpdateVertices();
            }
        }

        public UVMesh(Device device, Effect effect, RasterizerState drawMode, Material material, Color4 lineColor)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass");
            DrawMode = drawMode;
            SourceMaterial = material;
            this.lineColor = lineColor;

            if (SourceMaterial is null)
                return;

            CreateVertexLayout();

            CreateVertexBuffer();
            CreateIndexBuffer();
        }

        public void Prepare()
        {
            if (SourceMaterial is null)
                return;

            // 描画方式を設定
            UsingEffectPass.Apply(Device.ImmediateContext);
            Device.ImmediateContext.Rasterizer.State = DrawMode;
            Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // レイアウトを設定
            Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;

            // バッファを設定
            Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexStruct.SizeInBytes, 0)
            );
            Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);

            for (int i = 0; i < SourceMaterial.Faces.Count; i++)
            {
                Device.ImmediateContext.DrawIndexed(3, i * 3, 0);
            }
        }

        public void UpdateVertices()
        {
            if (SourceMaterial is null)
                return;
            CreateVertexBuffer();
        }

        void CreateVertexLayout()
        {
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                VertexStruct.VertexElements
            );
        }

        void CreateVertexBuffer()
        {
            using (var vertexStream = new DataStream(LoadUVVertices(SourceMaterial), true, true))
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
            using (DataStream indexStream = new DataStream(SourceMaterial.FaceSequence, true, true))
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

        private VertexStruct[] LoadUVVertices(Material material) =>
            material.Vertices.Select(vtx => new VertexStruct()
            {
                Position = new Vector3(vtx.UV.X, vtx.UV.Y, 0),
                Color = LineColor,
                TEXCOORD = vtx.UV
            }
            ).ToArray();


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
        // ~UVMesh()
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
