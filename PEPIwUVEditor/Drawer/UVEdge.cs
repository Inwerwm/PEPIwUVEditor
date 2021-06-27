using PEPlugin.Pmx;
using System.Collections.Generic;

namespace IwUVEditor.Drawer
{
    class UVEdge
    {
        public IPXVertex[] VertexPair { get; }

        public UVEdge(IPXVertex vertex1, IPXVertex vertex2)
        {
            VertexPair = new IPXVertex[2] { vertex1, vertex2 };
        }

        public static IEnumerable<UVEdge> FromFace(IPXFace face) => new[]
        {
            new UVEdge(face.Vertex1, face.Vertex2),
            new UVEdge(face.Vertex2, face.Vertex3),
            new UVEdge(face.Vertex3, face.Vertex1)
        };
    }
}
