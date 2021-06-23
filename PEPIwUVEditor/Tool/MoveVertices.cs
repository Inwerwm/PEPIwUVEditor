using IwUVEditor.Command;
using IwUVEditor.DirectX;
using IwUVEditor.Controller;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
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
        private MoveController Controller { get; }

        protected override Matrix Offset => Matrix.Translation(new Vector3(CurrentPos - StartPos, 0));

        public MoveVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process) {
            Controller = new MoveController(process, device);
        }

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
