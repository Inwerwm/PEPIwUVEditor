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
        protected override Matrix Offset
        {
            get
            {
                Vector3 trans = Controller.CurrentMode == EditController.SelectionMode.X ? new Vector3(CurrentPos.X - StartPos.X, 0, 0)
                              : Controller.CurrentMode == EditController.SelectionMode.Y ? new Vector3(0, CurrentPos.Y - StartPos.Y, 0)
                              : new Vector3(CurrentPos - StartPos, 0);
                return Matrix.Translation(trans);
            }
        }

        public MoveVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process, new MoveController(process, device)) {}

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
    }
}
