using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class RotationCenterSign : DrawElement<VectorOffset>, IDrawElement
    {
        private Vector3 center;
        private float radius;
        private Color4 color;
        private Vector2 screenSize;

        internal Vector3 Center
        {
            get => center;
            set
            {
                center = value;
                UpdateVertices();
            }
        }

        internal float Radius
        {
            get => radius;
            set
            {
                radius = value;
                UpdateVertices();
            }
        }

        public Color4 Color
        {
            get => color;
            set
            {
                color = value;
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

        public RotationCenterSign(Device device, Effect effect, RasterizerState drawMode, Vector3 center, float radius, Color4 color, Vector2 screenSize) :
            base(device, effect.GetTechniqueByName("VectorOffsetTechnique").GetPassByName("DrawRotationCenterPass"), drawMode)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.screenSize = screenSize;

            Initialize();
        }

        protected override void DrawToDevice()
        {
            Device.ImmediateContext.DrawIndexed(3, 0, 0);
            Device.ImmediateContext.DrawIndexed(3, 3, 0);
        }

        protected override VectorOffset[] CreateVertices()
        {
            Vector2 aspectCorrection = new Vector2(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1
            );
            return new[] {
                new VectorOffset
                {
                    Position = Center,
                    Color = Color,
                    Offset = new Vector3(-Radius * aspectCorrection.X, Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(0, 1),
                    AlphaRatio = 1,
                },
                new VectorOffset
                {
                    Position = Center,
                    Color = Color,
                    Offset = new Vector3(Radius * aspectCorrection.X, Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(1, 1),
                    AlphaRatio = 1,
                },
                new VectorOffset
                {
                    Position = Center,
                    Color = Color,
                    Offset = new Vector3(-Radius * aspectCorrection.X, -Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(0 ,0),
                    AlphaRatio = 1,
                },
                new VectorOffset
                {
                    Position = Center,
                    Color = Color,
                    Offset = new Vector3(Radius * aspectCorrection.X, -Radius * aspectCorrection.Y, 0),
                    TEXCOORD = new Vector2(1, 0),
                    AlphaRatio = 1,
                },
            };
        }

        protected override uint[] CreateIndices()
        {
            return new uint[] {
                0, 1, 2,
                3, 2, 1
            };
        }
    }
}
