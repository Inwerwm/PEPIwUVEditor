using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandApplyVertexEdit : IEditorCommand
    {
        public bool IsDestructive => true;

        IEnumerable<IPXVertex> TargetVertices { get; }
        Matrix Offset { get; }
        Dictionary<IPXVertex, PEPlugin.SDX.V2> PreviousUV { get; set; }

        public CommandApplyVertexEdit(IEnumerable<IPXVertex> targetVertices, Matrix offset)
        {
            TargetVertices = targetVertices;
            Offset = offset;
        }

        public void Do()
        {
            PreviousUV = TargetVertices.AsParallel().ToDictionary(v => v, v => v.UV.Clone());
            TargetVertices.AsParallel().ForAll(vtx => vtx.UV = Vector2.TransformCoordinate(vtx.UV, Offset));
        }

        public void Undo()
        {
            TargetVertices.AsParallel().ForAll(vtx => vtx.UV = PreviousUV[vtx]);
        }
    }
}
