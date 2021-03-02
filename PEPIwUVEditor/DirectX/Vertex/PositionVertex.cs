using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;

namespace IwUVEditor.DirectX.Vertex
{
    struct PositionVertex : IDxVertex
    {
        public Vector3 Position;

        public IDxVertex Instance => new PositionVertex();

        public InputElement[] VertexElements => new[]
        {
            new InputElement("SV_Position", 0, Format.R32G32B32_Float,    0,                          0, InputClassification.PerVertexData,   0),
        };

        public int SizeInBytes => System.Runtime.InteropServices.Marshal.SizeOf(typeof(PositionVertex));
    }
}
