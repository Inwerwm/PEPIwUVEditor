using IwUVEditor.Controller;
using IwUVEditor.DirectX;
using SlimDX;

namespace IwUVEditor.Tool
{
    class MoveVertices : EditVertices, IEditTool
    {
        protected override Matrix Offset =>
            Matrix.Translation(Parameters.MoveOffset);

        public MoveVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process, IEditParameter parameters) : base(process, new MoveController(process, device, parameters), parameters) { }

        protected override void UpdateParameter()
        {
            Parameters.MoveOffset = Controller.CurrentMode == EditController.SelectionMode.X ? new Vector3(CurrentPos.X - StartPos.X, 0, 0)
                                  : Controller.CurrentMode == EditController.SelectionMode.Y ? new Vector3(0, CurrentPos.Y - StartPos.Y, 0)
                                  : new Vector3(CurrentPos - StartPos, 0);
            Parameters.MoveOffset *= Input.ModifierRatio;
        }

        public override void Initialize()
        {
            base.Initialize();
            Controller.Center = CenterPos;
        }
    }
}
