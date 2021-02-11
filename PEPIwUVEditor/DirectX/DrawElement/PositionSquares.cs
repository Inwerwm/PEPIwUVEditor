using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class PositionSquares : IDrawElement
    {
        private bool disposedValue;
        private float radius;
        private Color4 colorInSelected;

        Device Device { get; }
        EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        InputLayout VertexLayout { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }
        Buffer InstanceBuffer { get; set; }

        Material SourceMaterial { get; }
        PositionVertex[] SquareVertices { get; set; }
        uint[] SquareIndices { get; }
        List<PositionSquareVertex> Instances { get; set; }

        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                CreateVertexBuffer();
            }
        }

        /// <summary>
        /// 選択状態の頂点色
        /// </summary>
        public Color4 ColorInSelected
        {
            get => colorInSelected;
            set
            {
                colorInSelected = value;
                UpdateVertices();
            }
        }
        /// <summary>
        /// 非選択状態の頂点色
        /// </summary>
        public Color4 ColorInNotSelected { get; set; }

        public PositionSquares(Device device, Effect effect, RasterizerState drawMode, Material material, float radius)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("PositionSquaresTechnique").GetPassByName("DrawPositionSquaresPass");
            DrawMode = drawMode;
            SourceMaterial = material;
            this.radius = radius;

            if (SourceMaterial is null)
                return;

            SquareIndices = CreateConstantData();

            CreateVertexLayout();

            CreateVertexBuffer();
            CreateIndexBuffer();
            CreateInstanceBuffer();
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
                new VertexBufferBinding(VertexBuffer, PositionVertex.SizeInBytes, 0),
                new VertexBufferBinding(InstanceBuffer, PositionSquareVertex.SizeInBytes, 0)
            );
            Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);

            // 描画を設定
            Device.ImmediateContext.DrawIndexedInstanced(3, Instances.Count, 0, 0, 0);
            Device.ImmediateContext.DrawIndexedInstanced(3, Instances.Count, 3, 0, 0);
        }

        public void UpdateVertices()
        {
            CreateInstances();
        }

        private void CreateVertexLayout()
        {
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                PositionVertex.VertexElements.Concat(PositionSquareVertex.VertexElements).ToArray()
            );
        }

        private void CreateVertexBuffer()
        {
            CreateSquareVertices();

            using (var vertexStream = new DataStream(SquareVertices, true, true))
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

        private void CreateIndexBuffer()
        {
            using (DataStream indexStream = new DataStream(SquareIndices, true, true))
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

        private void CreateInstanceBuffer()
        {
            CreateInstances();

            using (DataStream instanceStream = new DataStream(Instances.ToArray(), false, true))
                InstanceBuffer = new Buffer(
                    Device,
                    instanceStream,
                    new BufferDescription
                    (
                        PositionSquareVertex.SizeInBytes * Instances.Count,
                        ResourceUsage.Dynamic,
                        BindFlags.VertexBuffer,
                        CpuAccessFlags.Write,
                        ResourceOptionFlags.None,
                        0
                    )
                );
        }

        private void CreateSquareVertices()
        {
            SquareVertices = new[] {
                new PositionVertex
                {
                    Position = new Vector3(-1, 1, 0) * Radius,
                },
                new PositionVertex
                {
                    Position = new Vector3(1, 1, 0) * Radius,
                },
                new PositionVertex
                {
                    Position = new Vector3(-1, -1, 0) * Radius,
                },
                new PositionVertex
                {
                    Position = new Vector3(1, -1, 0) * Radius,
                },
            };
        }

        private void CreateInstances()
        {
            Instances = SourceMaterial.Vertices.Select(vtx =>
                new PositionSquareVertex()
                {
                    Color = SourceMaterial.IsSelected[vtx] ? ColorInSelected : ColorInNotSelected,
                    Offset = new Vector4(vtx.UV.X * 2 - 1, 1 - vtx.UV.Y * 2, 0, 1),
                }
            ).ToList();
        }

        private uint[] CreateConstantData()
        {
            return new uint[] {
                0, 1, 2,
                3, 2, 1
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~PositionSquare()
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
