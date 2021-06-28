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

        public UVEdge Mul(float width, float height) =>
            new UVEdge(
                new PointF(UV[0].X * width, UV[0].Y * height),
                new PointF(UV[1].X * width, UV[1].Y * height)
            );

        public UVEdge Add(float width, float height) =>
            new UVEdge(
                new PointF(UV[0].X + width, UV[0].Y + height),
                new PointF(UV[1].X + width, UV[1].Y + height)
            );
    }
}
