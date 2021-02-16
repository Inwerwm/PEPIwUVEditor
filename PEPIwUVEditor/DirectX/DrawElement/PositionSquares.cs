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
        private Color4 colorInDefault;
        private Vector2 screenSize;

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
                UpdateVertices();
            }
        }

        public Vector2 ScreenSize
        {
            get => screenSize;
            set
            {
                screenSize = value;
                UpdateVertices();
            }
        }

        /// <summary>
        /// 非選択状態の頂点色
        /// </summary>
        public Color4 ColorInDefault
        {
            get => colorInDefault;
            set
            {
                colorInDefault = value;
                UpdateVertices();
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

        public PositionSquares(Device device, Effect effect, RasterizerState drawMode, Material material, float radius, Color4 colorInDefault, Color4 colorInSelected)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("PositionSquaresTechnique").GetPassByName("DrawPositionSquaresPass");
            DrawMode = drawMode;
            SourceMaterial = material;

            this.radius = radius;
            this.colorInDefault = colorInDefault;
            this.colorInSelected = colorInSelected;

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
            if (SourceMaterial is null)
                return;

            CreateVertexBuffer();
            CreateInstanceBuffer();
        }

        private void CreateVertexLayout()
        {
            VertexLayout?.Dispose();
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                PositionVertex.VertexElements.Concat(PositionSquareVertex.VertexElements).ToArray()
            );
        }

        private void CreateVertexBuffer()
        {
            CreateSquareVertices();

            VertexBuffer?.Dispose();
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
            IndexBuffer?.Dispose();
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

            InstanceBuffer?.Dispose();
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
            Vector2 aspectCorrection = new Vector2(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1
            );

            SquareVertices = new[] {
                new PositionVertex
                {
                    Position = new Vector3(-1 * aspectCorrection.X, 1 * aspectCorrection.Y, 0) * Radius,
                },
                new PositionVertex
                {
                    Position = new Vector3(1 * aspectCorrection.X, 1 * aspectCorrection.Y, 0) * Radius,
                },
                new PositionVertex
                {
                    Position = new Vector3(-1 * aspectCorrection.X, -1 * aspectCorrection.Y, 0) * Radius,
                },
                new PositionVertex
                {
                    Position = new Vector3(1 * aspectCorrection.X, -1 * aspectCorrection.Y, 0) * Radius,
                },
            };
        }

        private void CreateInstances()
        {
            Instances = SourceMaterial.Vertices.Select(vtx =>
                new PositionSquareVertex()
                {
                    Color = SourceMaterial.IsSelected[vtx] ? ColorInSelected : ColorInDefault,
                    Offset = new Vector4(vtx.UV.X, vtx.UV.Y, 0, 1),
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
                    VertexLayout?.Dispose();
                    VertexBuffer?.Dispose();
                    IndexBuffer?.Dispose();
                    InstanceBuffer?.Dispose();
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
