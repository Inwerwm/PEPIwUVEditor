using IwUVEditor.Command;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor.Tool
{
    class MoveVertices : EditVertices, IEditTool
    {
        protected override Matrix Offset => Matrix.Translation(new Vector3(CurrentPos - StartPos, 0));

        public MoveVertices(UVViewDrawProcess process) : base(process) { }
    }
}
