using IwUVEditor.DirectX;
using IwUVEditor.Controller;
using IwUVEditor.StateContainer;
using SlimDX;

namespace IwUVEditor.Tool
{
    class ScaleVertices : EditVertices, IEditTool
    {
        private static float Step => 0.01f;

        protected override Matrix Offset
        {
            get
            {
                Matrix currentScale;
                switch (Controller.CurrentMode)
                {
                    case EditController.SelectionMode.X:
                        currentScale = Matrix.Scaling(new Vector3(1 + Input.MouseOffset.X * Step, 1, 1));
                        break;
                    case EditController.SelectionMode.Y:
                        currentScale = Matrix.Scaling(new Vector3(1, 1 - Input.MouseOffset.Y * Step, 1));
                        break;
                    default:
                        currentScale = Matrix.Scaling(new Vector3(1 + Input.MouseOffset.X * Step, 1 - Input.MouseOffset.Y * Step, 1));
                        break;
                }

                Matrix centerOffset = Matrix.Translation(Controller.Center);
                return Matrix.Invert(centerOffset) * currentScale * centerOffset;
            }
        }

        public ScaleVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process, new ScaleController(process, device)){}

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
