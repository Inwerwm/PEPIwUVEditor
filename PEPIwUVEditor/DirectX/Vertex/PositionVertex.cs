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
    struct PositionVertex
    {
        public Vector3 Position;

        public static readonly InputElement[] VertexElements = new[]
        {
            new InputElement("SV_Position", 0, Format.R32G32B32_Float,    0,                          0, InputClassification.PerVertexData,   0),
        };

        public static int SizeInBytes
        {
            get
            {
                return System.Runtime.InteropServices.
                    Marshal.SizeOf(typeof(PositionVertex));
            }
        }
    }
}
