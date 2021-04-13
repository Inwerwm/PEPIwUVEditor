using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class SelectionRectangle : DrawElement<VertexStruct>, IDrawElement
    {
        private Color4 color;

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

        public SelectionRectangle(Device device, Effect effect, RasterizerState drawMode, Color4 color) :
            base(device, effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass"), drawMode)
        {
            this.color = color;

            Initialize();
        }

        public override void Prepare()
        {
            CreateVertexBuffer();
            base.Prepare();
        }

        protected override void DrawToDevice()
        {
            Device.ImmediateContext.DrawIndexed(3, 0, 0);
            Device.ImmediateContext.DrawIndexed(3, 3, 0);
        }

        protected override VertexStruct[] CreateVertices()
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

        protected override uint[] CreateIndices() => new uint[] {
            0, 1, 2,
            3, 2, 1
        };
    }
}