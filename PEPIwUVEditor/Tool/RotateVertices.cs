using IwUVEditor.DirectX;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.Controller;
using IwUVEditor.StateContainer;
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
        protected override Matrix Offset =>
            Matrix.Translation(Controller.Center * -1) * Matrix.RotationZ((float)(Step * Input.MouseOffset.Y * 2 * Math.PI)) * Matrix.Translation(Controller.Center);

        public RotateVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process, new RotateController(process, device)){}

        public override void Initialize()
        {
            base.Initialize();
            Controller.Center = CenterPos;
        }

        public override void PrepareDrawing()
        {
            base.PrepareDrawing();
            Controller.PrepareDrawing();
        }

        public override void ReadInput(InputStates input)
        {
            Controller.ReadInput(input, base.ReadInput);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Controller?.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
