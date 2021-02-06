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
    struct VertexStruct
    {
        public Vector3 Position;
        public Color4 Color;
        public Vector2 TEXCOORD;

        public static readonly InputElement[] VertexElements = new[]
        {
            new InputElement
            {
                SemanticName = "SV_Position",
                Format = Format.R32G32B32_Float
            },
            new InputElement
            {
                SemanticName = "Color",
                Format = Format.R32G32B32A32_Float,
                AlignedByteOffset = InputElement.AppendAligned//自動的にオフセット決定
            },
            new InputElement
            {
                SemanticName = "TEXCOORD",
                Format = Format.R32G32_Float,
                AlignedByteOffset = InputElement.AppendAligned//自動的にオフセット決定
            }
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
