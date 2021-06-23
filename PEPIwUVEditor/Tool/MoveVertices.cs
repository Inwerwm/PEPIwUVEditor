using IwUVEditor.Controller;
using IwUVEditor.DirectX;
using SlimDX;

namespace IwUVEditor.Tool
{
    class MoveVertices : EditVertices, IEditTool
    {
        protected override Matrix Offset =>
            Matrix.Translation(Controller.CurrentMode == EditController.SelectionMode.X ? new Vector3(CurrentPos.X - StartPos.X, 0, 0)
                             : Controller.CurrentMode == EditController.SelectionMode.Y ? new Vector3(0, CurrentPos.Y - StartPos.Y, 0)
                             : new Vector3(CurrentPos - StartPos, 0));

        public MoveVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process, new MoveController(process, device)) { }
    }
}
