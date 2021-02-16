using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;

namespace IwUVEditor.DirectX.Vertex
{
    struct VertexStruct
    {
        public Vector3 Position;
        public Color4 Color;
        public Vector2 TEXCOORD;

        public static readonly InputElement[] VertexElements = new[]
        {
            new InputElement("SV_Position", 0, Format.R32G32B32_Float,    0,                          0, InputClassification.PerVertexData,   0),
            new InputElement("Color",       0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData,   0),
            new InputElement("TEXCOORD",    0, Format.R32G32_Float,       InputElement.AppendAligned, 0, InputClassification.PerVertexData,   0),
        };

        public static int SizeInBytes
        {
            get
            {
                return System.Runtime.InteropServices.
                    Marshal.SizeOf(typeof(VertexStruct));
            }
        }
    }
}
