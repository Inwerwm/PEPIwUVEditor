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
                float xScale = 1 + Input.MouseOffset.X * Step;
                float yScale = 1 - Input.MouseOffset.Y * Step;

                Matrix currentScale = Controller.CurrentMode == EditController.SelectionMode.X ? Matrix.Scaling(new Vector3(xScale, 1, 1))
                                    : Controller.CurrentMode == EditController.SelectionMode.Y ? Matrix.Scaling(new Vector3(1, yScale, 1))
                                    :                                                            Matrix.Scaling(new Vector3(xScale, yScale, 1));
                Matrix centerOffset = Matrix.Translation(Controller.Center);

                return Matrix.Invert(centerOffset) * currentScale * centerOffset;
            }
        }

        public ScaleVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process, new ScaleController(process, device)) { }
    }
}
