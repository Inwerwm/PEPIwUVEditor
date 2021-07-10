using PEPlugin.Pmx;
using PEPlugin.SDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandMoveVerticesByMorph : IEditorCommand
    {
        public bool IsDestructive => true;

        private IEnumerable<IPXUVMorphOffset> Offsets { get; }
        private IEnumerable<(IPXVertex Vertex, V2 UV)> PreviousPositions { get; }

        public CommandMoveVerticesByMorph(IEnumerable<IPXUVMorphOffset> offsets)
        {
            Offsets = offsets;
            PreviousPositions = offsets.Select(o => (o.Vertex, o.Vertex.UV.Clone())).ToArray();
        }

        public void Do()
        {
            foreach (var item in Offsets)
            {
                item.Vertex.UV.X += item.Offset.X;
                item.Vertex.UV.Y += item.Offset.Y;
            }
        }

        public void Undo()
        {
            foreach (var item in PreviousPositions)
            {
                item.Vertex.UV = item.UV;
            }
        }
    }
}
