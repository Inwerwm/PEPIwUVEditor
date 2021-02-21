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
        /// <summary>
        /// 回転量/移動量
        /// </summary>
        private static double Step => 0.001;
        protected override Matrix Offset
        {
            get
            {
                return Matrix.Translation(CenterPos * -1) * Matrix.RotationZ((float)(Step * Input.MouseOffset.Y * 2 * Math.PI)) * Matrix.Translation(CenterPos);
            }
        }


        public RotateVertices(UVViewDrawProcess process) : base(process) { }
    }
}
