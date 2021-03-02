using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System.Linq;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class PositionSquares : DrawElementInstanced<PositionVertex, PositionSquareVertex>, IDrawElement
    {
        private float radius;
        private Color4 colorInSelected;
        private Color4 colorInDefault;
        private Vector2 screenSize;

        Material SourceMaterial { get; }
        int InstanceCount { get; set; }

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

        public PositionSquares(Device device, Effect effect, RasterizerState drawMode, Material material, float radius, Color4 colorInDefault, Color4 colorInSelected) :
            base(device, effect.GetTechniqueByName("PositionSquaresTechnique").GetPassByName("DrawPositionSquaresPass"), drawMode)
        {
            SourceMaterial = material;
            if (SourceMaterial is null)
                return;

            this.radius = radius;
            this.colorInDefault = colorInDefault;
            this.colorInSelected = colorInSelected;

            Initialize();
        }

        public override void Prepare()
        {
            if (SourceMaterial is null)
                return;

            base.Prepare();
        }

        protected override void DrawToDevice()
        {
            Device.ImmediateContext.DrawIndexedInstanced(3, InstanceCount, 0, 0, 0);
            Device.ImmediateContext.DrawIndexedInstanced(3, InstanceCount, 3, 0, 0);
        }

        public override void UpdateVertices()
        {
            if (SourceMaterial is null)
                return;

            CreateVertexBuffer();
            CreateInstanceBuffer();
        }

        protected override PositionVertex[] CreateVertices()
        {
            Vector2 aspectCorrection = new Vector2(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1
            );

            return new[] {
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

        protected override uint[] CreateIndices() => new uint[] {
            0, 1, 2,
            3, 2, 1
        };

        protected override PositionSquareVertex[] CreateInstances()
        {
            PositionSquareVertex[] instences = SourceMaterial.Vertices.Select(vtx =>
                new PositionSquareVertex()
                {
                    Color = SourceMaterial.IsSelected[vtx] ? ColorInSelected : ColorInDefault,
                    Offset = Vector4.Transform(new Vector4(vtx.UV.X, vtx.UV.Y, 0, 1), SourceMaterial.TemporaryTransformMatrices[vtx]),
                }
            ).ToArray();
            InstanceCount = instences.Length;

            return instences;
        }
    }
}
