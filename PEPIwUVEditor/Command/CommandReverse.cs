using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandReverse : IEditorCommand
    {
        public bool IsDestructive => true;

        IEnumerable<IPXVertex> TargetVertices { get; }

        Axis ReverseAxis { get; }

        public CommandReverse(IEnumerable<IPXVertex> targetVertices, Axis axis)
        {
            TargetVertices = targetVertices;
            ReverseAxis = axis;
        }

        public void Do()
        {
            (var min, var max) = TargetVertices.Select(vtx => vtx.UV.ToVector2()).MinMax();
            var center = ((min + max) / 2).ToVector3();

            bool XorBoth = ReverseAxis != Axis.Y;
            bool YorBoth = ReverseAxis != Axis.X;

            Matrix reverser = Matrix.Invert(Matrix.Translation(center)) * Matrix.Scaling(XorBoth ? -1 : 1, YorBoth ? -1 : 1, 1) * Matrix.Translation(center);
            TargetVertices.AsParallel().ForAll(vtx => vtx.UV = Vector2.TransformCoordinate(vtx.UV, reverser));
        }

        public void Undo()
        {
            Do();
        }

        public enum Axis
        {
            X,
            Y,
            Both
        }
    }
}
