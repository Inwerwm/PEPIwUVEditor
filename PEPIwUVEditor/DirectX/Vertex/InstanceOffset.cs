using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;

namespace IwUVEditor.DirectX.Vertex
{
    struct InstanceOffset : IDxVertex
    {
        public Matrix Offset;
        public float AlphaRatio;

        public IDxVertex Instance => new InstanceOffset();
        public InputElement[] VertexElements => new[]
        {
            new InputElement("Offset",      0, Format.R32G32B32A32_Float, 0,                          1, InputClassification.PerInstanceData, 1),
            new InputElement("Offset",      1, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1),
            new InputElement("Offset",      2, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1),
            new InputElement("Offset",      3, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1),
            new InputElement("Ratio",       0, Format.R32_Float         , InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1)
        };

        public int SizeInBytes => System.Runtime.InteropServices.Marshal.SizeOf(typeof(InstanceOffset));
    }
}
