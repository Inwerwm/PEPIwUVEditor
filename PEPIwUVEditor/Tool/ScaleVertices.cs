using IwUVEditor.Controller;
using IwUVEditor.DirectX;
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
                Matrix centerOffset = Matrix.Translation(Controller.Center);

                return Matrix.Invert(centerOffset) * Matrix.Scaling(Parameters.ScaleRatio) * centerOffset;
            }
        }

        public ScaleVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process, IEditParameter parameters) : base(process, new ScaleController(process, device), parameters) { }

        protected override void UpdateParameter()
        {
            float xScale = 1 + Input.MouseOffset.X * Step;
            float yScale = 1 - Input.MouseOffset.Y * Step;

            Parameters.ScaleRatio = Controller.CurrentMode == EditController.SelectionMode.X ? new Vector3(xScale, 1, 1)
                                  : Controller.CurrentMode == EditController.SelectionMode.Y ? new Vector3(1, yScale, 1)
                                  : new Vector3(xScale, yScale, 1);
        }
    }
}
