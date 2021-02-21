using IwUVEditor.DirectX;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    class RotateVertices : EditVertices, IEditTool
    {
        protected override Matrix Offset => Matrix.RotationZ(CurrentPos.Y - StartPos.Y);

        public RotateVertices(UVViewDrawProcess process) : base(process) { }
    }
}
