using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandSetPosition : IEditorCommand
    {
        public bool IsDestructive => true;

        List<IPXVertex> TargetVertices { get; }
        Dictionary<IPXVertex, Vector2> PreviousPosition { get; }
        Vector2 Positon { get; }

        public CommandSetPosition(List<IPXVertex> targetVertices, Vector2 positon)
        {
            TargetVertices = targetVertices;
            PreviousPosition = TargetVertices.ToDictionary(vtx => vtx, vtx => (Vector2)vtx.UV);
            Positon = positon;
        }

        public void Do()
        {
            foreach (var vtx in TargetVertices)
            {
                vtx.UV = Positon;
            }
        }

        public void Undo()
        {
            foreach (var vtx in TargetVertices)
            {
                vtx.UV = PreviousPosition[vtx];
            }
        }
    }
}
