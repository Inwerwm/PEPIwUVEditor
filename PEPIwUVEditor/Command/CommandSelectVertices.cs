using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandSelectVertices : IEditorCommand
    {
        Material TargetMaterial { get; }
        Dictionary<IPXVertex, bool> VertexSelect { get; }
        Dictionary<IPXVertex, bool> PreviousState { get; set; }
        public SelectionMode Mode { get; set; }

        public CommandSelectVertices(Material targetMaterial, Dictionary<IPXVertex, bool> vertexSelect)
        {
            TargetMaterial = targetMaterial;
            VertexSelect = vertexSelect;
        }

        public void Do()
        {
            PreviousState = TargetMaterial.IsSelected.ToDictionary(p => p.Key, p => p.Value);
            foreach (var sel in VertexSelect)
            {
                switch (Mode)
                {
                    case SelectionMode.Create:
                        TargetMaterial.IsSelected[sel.Key] = sel.Value;
                        break;
                    case SelectionMode.Union:
                        TargetMaterial.IsSelected[sel.Key] |= sel.Value;
                        break;
                    case SelectionMode.Difference:
                        TargetMaterial.IsSelected[sel.Key] &= !sel.Value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void Undo()
        {
            foreach (var state in PreviousState)
            {
                TargetMaterial.IsSelected[state.Key] = state.Value;
            }
        }
    }
}
