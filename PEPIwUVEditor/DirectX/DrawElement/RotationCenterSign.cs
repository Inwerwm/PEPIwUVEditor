using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class RotationCenterSign : IDrawElement
    {
        private bool disposedValue;
        private Vector3 center;
        private float radius;
        private Color4 color;
        private Vector2 screenSize;

        private int instanceCount = 4;

        Device Device { get; }
        EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        InputLayout VertexLayout { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }
        Buffer InstanceBuffer { get; set; }

        internal Vector3 Center
        {
            get => center;
            set
            {
                center = value;
                CreateVertexBuffer();
            }
        }

        internal float Radius
        {
            get => radius;
            set
            {
                radius = value;
                CreateInstanceBuffer();
            }
        }

        public Color4 Color
        {
            get => color;
            set
            {
                color = value;
                CreateInstanceBuffer();
            }
        }

        public Vector2 ScreenSize
        {
            get => screenSize;
            set
            {
                screenSize = value;
                CreateInstanceBuffer();
            }
        }

        public RotationCenterSign(Device device, Effect effect, RasterizerState drawMode, Vector3 center, float radius, Color4 color, Vector2 screenSize)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("RotationCenterTechnique").GetPassByName("DrawRotationCenterPass");
            DrawMode = drawMode;
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.screenSize = screenSize;

            CreateVertexLayout();

            CreateVertexBuffer();
            CreateIndexBuffer();
            CreateInstanceBuffer();
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
            Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, PositionVertex.SizeInBytes, 0),
                new VertexBufferBinding(InstanceBuffer, VectorOffset.SizeInBytes, 0)
            );
            Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);

            // 描画を設定
            Device.ImmediateContext.DrawIndexedInstanced(3, instanceCount, 0, 0, 0);
            Device.ImmediateContext.DrawIndexedInstanced(3, instanceCount, 3, 0, 0);
        }

        public void UpdateVertices()
        {
            CreateVertexBuffer();
            CreateInstanceBuffer();
        }

        private void CreateVertexLayout()
        {
            VertexLayout?.Dispose();
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                PositionVertex.VertexElements.Concat(VectorOffset.VertexElements).ToArray()
            );
        }

        private void CreateVertexBuffer()
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

        private void CreateIndexBuffer()
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

        private void CreateInstanceBuffer()
        {
            InstanceBuffer?.Dispose();
            using (DataStream instanceStream = new DataStream(CreateInstances(), false, true))
                InstanceBuffer = new Buffer(
                    Device,
                    instanceStream,
                    new BufferDescription
                    (
                        (int)instanceStream.Length,
                        ResourceUsage.Dynamic,
                        BindFlags.VertexBuffer,
                        CpuAccessFlags.Write,
                        ResourceOptionFlags.None,
                        0
                    )
                );
        }

        private PositionVertex[] CreateVertices()
        {
            return new[] {
                new PositionVertex
                {
                    Position = Center,
                }
            };
        }

        private VectorOffset[] CreateInstances()
        {
            Vector2 aspectCorrection = new Vector2(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1
            );

            return new[] {
                new VectorOffset
                {
                    Color = Color,
                    Offset = new Vector3(-Radius * aspectCorrection.X, Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(0, 1),
                    AlphaRatio = 1,
                },
                new VectorOffset
                {
                    Color = Color,
                    Offset = new Vector3(Radius * aspectCorrection.X, Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(1, 1),
                    AlphaRatio = 1,
                },
                new VectorOffset
                {
                    Color = Color,
                    Offset = new Vector3(-Radius * aspectCorrection.X, -Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(0 ,0),
                    AlphaRatio = 1,
                },
                new VectorOffset
                {
                    Color = Color,
                    Offset = new Vector3(Radius * aspectCorrection.X, -Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(1, 0),
                    AlphaRatio = 1,
                },
            };
        }

        private uint[] CreateIndices()
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
        // ~RotationCenterSign()
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
