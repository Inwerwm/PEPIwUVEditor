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
    struct PositionSquareVertex
    {
        public Vector4 Offset;
        public float AlphaRatio;

        public static readonly InputElement[] VertexElements = new[]
        {
            new InputElement("Offset",      0, Format.R32G32B32A32_Float, 0,                          1, InputClassification.PerInstanceData, 1),
            new InputElement("Ratio",       0, Format.R32_Float         , InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1)
        };

        public static int SizeInBytes
        {
            get
            {
                return System.Runtime.InteropServices.
                    Marshal.SizeOf(typeof(PositionSquareVertex));
            }
        }
    }
}
