using PEPlugin.Pmx;
using System.Collections.Generic;
using System.Drawing;

namespace IwUVEditor.Drawer
{
    class UVEdge
    {
        public PointF[] UV { get; }

        public UVEdge(IPXVertex vertex1, IPXVertex vertex2)
        {
            UV = new[] { new PointF(vertex1.UV.X, vertex1.UV.Y), new PointF(vertex2.UV.X, vertex2.UV.Y) };
        }

        private UVEdge(PointF point1, PointF point2)
        {
            UV = new[] { point1, point2 };
        }

        public static IEnumerable<UVEdge> FromFace(IPXFace face) => new[]
        {
            new UVEdge(face.Vertex1, face.Vertex2),
            new UVEdge(face.Vertex2, face.Vertex3),
            new UVEdge(face.Vertex3, face.Vertex1)
        };

        public static UVEdge operator *(UVEdge edge, float ratio) =>
            new UVEdge(
                new PointF(edge.UV[0].X * ratio, edge.UV[0].Y * ratio),
                new PointF(edge.UV[1].X * ratio, edge.UV[1].Y * ratio)
            );
    }
}
