using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.DirectX.Vertex
{
    struct VectorOffset : IDxVertex
    {
        public Vector3 Position;
        public Color4 Color;
        public Vector3 Offset;
        public Vector2 TEXCOORD;
        public float AlphaRatio;

        public IDxVertex Instance => new VectorOffset();

        public InputElement[] VertexElements => new[]
        {
            new InputElement("SV_Position", 0, Format.R32G32B32_Float,    0,                          0, InputClassification.PerVertexData, 0),
            new InputElement("Color",       0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0),
            new InputElement("Offset",      0, Format.R32G32B32_Float,    InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0),
            new InputElement("TEXCOORD",    0, Format.R32G32_Float,       InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0),
            new InputElement("Ratio",       0, Format.R32_Float,          InputElement.AppendAligned, 0, InputClassification.PerVertexData, 0)
        };

        public int SizeInBytes => System.Runtime.InteropServices.Marshal.SizeOf(typeof(VectorOffset));
    }
}
