using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;

namespace IwUVEditor.DirectX.Vertex
{
    struct PositionSquareVertex : IDxVertex
    {
        public Color4 Color;
        public Vector4 Offset;

        public InputElement[] VertexElements => new[]
        {
            new InputElement("Color",  0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1),
            new InputElement("Offset", 0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1),
        };

        public int SizeInBytes => System.Runtime.InteropServices.Marshal.SizeOf(typeof(PositionSquareVertex));
    }
}
