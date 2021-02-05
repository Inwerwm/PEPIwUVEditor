using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    struct VertexPositionColor
    {
        public Vector3 Position;
        public Vector3 Color;

        public static readonly InputElement[] VertexElements = new[]
        {
            new InputElement
            {
                SemanticName = "SV_Position",
                Format = Format.R32G32B32_Float
            },
            new InputElement
            {
                SemanticName = "COLOR",
                Format = Format.R32G32B32_Float,
                AlignedByteOffset = InputElement.AppendAligned//自動的にオフセット決定
            }
        };

        public static int SizeInBytes
        {
            get
            {
                return System.Runtime.InteropServices.
                    Marshal.SizeOf(typeof(VertexPositionColor));
            }
        }
    }
}
