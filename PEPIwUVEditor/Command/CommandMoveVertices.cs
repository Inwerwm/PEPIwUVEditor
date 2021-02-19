using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandMoveVertices : IEditorCommand
    {
        List<IPXVertex> TargetVertices { get; }
        Matrix Offset { get; }

        public CommandMoveVertices(List<IPXVertex> targetVertices, Matrix offset)
        {
            TargetVertices = targetVertices;
            Offset = offset;
        }

        public void Do()
        {
            TargetVertices.AsParallel().ForAll(vtx => vtx.UV = Vector2.TransformCoordinate(vtx.UV, Offset));
        }

        public void Undo()
        {
            TargetVertices.AsParallel().ForAll(vtx => vtx.UV = Vector2.TransformCoordinate(vtx.UV, Matrix.Invert(Offset)));
        }
    }
}
