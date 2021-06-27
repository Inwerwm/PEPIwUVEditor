using PEPExtensions;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Drawer
{
    class UVMesh
    {
        public IEnumerable<UVEdge> Edges { get; }
        public IEnumerable<IPXVertex> Vertices { get; }

        public UVMesh(IPXMaterial material)
        {
            Vertices = material.Faces.SelectMany(face => face.ToVertices()).Distinct();
            Edges = material.Faces.SelectMany(UVEdge.FromFace);
        }
    }
}
