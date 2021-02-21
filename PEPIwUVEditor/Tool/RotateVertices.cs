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
        protected override Matrix Offset
        {
            get
            {
                return Matrix.Translation(CenterPos * -1) * Matrix.RotationZ((CurrentPos.Y - StartPos.Y) * (float)Math.PI * 2f) * Matrix.Translation(CenterPos);
            }
        }

        public RotateVertices(UVViewDrawProcess process) : base(process) { }
    }
}
