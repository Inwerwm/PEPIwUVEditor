using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CmdSelectSquare : IEditorCommand
    {
        Material TargetMaterial { get; }

        List<IPXVertex> SelectedVertices { get; }

        public Vector2? StartPosition { get; set; } = null;
        public Vector2? EndPosition { get; set; } = null;

        public CmdSelectSquare(Material targetMaterial)
        {
            TargetMaterial = targetMaterial;

            SelectedVertices = new List<IPXVertex>();
        }

        public void Do()
        {
            if (StartPosition is null)
                throw new InvalidOperationException("StartPosition is null");
            if (EndPosition is null)
                throw new InvalidOperationException("EndPositon is null");

            
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
