using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System.Linq;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class UVMesh : DrawElement<VertexStruct>, IDrawElement
    {
        private Color4 lineColor;

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

        public UVMesh(Device device, Effect effect, RasterizerState drawMode, Material material, Color4 lineColor) :
            base(device, effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass"), drawMode)
        {
            SourceMaterial = material;
            this.lineColor = lineColor;

            if (SourceMaterial is null)
                return;

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
            for (int i = 0; i < SourceMaterial.Faces.Count; i++)
            {
                Device.ImmediateContext.DrawIndexed(3, i * 3, 0);
            }
        }

        public override void UpdateVertices()
        {
            if (SourceMaterial is null)
                return;
            CreateVertexBuffer();
        }

        protected override VertexStruct[] CreateVertices() =>
            SourceMaterial.Vertices.Select(vtx => new VertexStruct()
            {
                Position = Vector3.TransformCoordinate(new Vector3(vtx.UV.X, vtx.UV.Y, 0), SourceMaterial.IsSelected[vtx] ? SourceMaterial.TemporaryTransformMatrices : Matrix.Identity),
                Color = LineColor,
                TEXCOORD = vtx.UV
            }
            ).ToArray();

        protected override uint[] CreateIndices() => SourceMaterial.FaceSequence;
    }
}
