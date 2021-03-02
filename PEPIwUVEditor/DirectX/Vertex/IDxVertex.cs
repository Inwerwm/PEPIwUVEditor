﻿using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.DirectX.Vertex
{
    interface IDxVertex
    {
        IDxVertex Instance { get; }
        InputElement[] VertexElements { get; }
        int SizeInBytes { get; }
    }
}
