using IwUVEditor.Controller;
using IwUVEditor.DirectX;
using SlimDX;
using System;

namespace IwUVEditor.Tool
{
    class ScaleVertices : EditVertices, IEditTool
    {
        private static float Step => 0.01f;
        private Vector3 PreviousScaleRatio { get; set; } = new Vector3(1, 1, 1);
        protected override Matrix Offset
        {
            get
            {
                Matrix centerOffset = Matrix.Translation(Parameters.ScaleCenter);

                return Matrix.Invert(centerOffset) * Matrix.Invert(Matrix.Scaling(PreviousScaleRatio)) * Matrix.Scaling(Parameters.ScaleRatio) * centerOffset;
            }
        }

        public ScaleVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process, IEditParameter parameters) : base(process, new ScaleController(process, device, parameters), parameters) { }

        protected override void UpdateParameter()
        {
            PreviousScaleRatio = Input.MouseLeft.IsStartingJust ? new Vector3(1, 1, 1) : Parameters.ScaleRatio;

            var currentDistanceFromCenter = Input.MouseLeft.Current - Parameters.ScaleCenter.ToVector2();
            var startDistanceFromCenter = Input.MouseLeft.Start - Parameters.ScaleCenter.ToVector2();

            var offset = currentDistanceFromCenter.ElementDivision(startDistanceFromCenter);
            offset = offset.Map((source) => (source - 1) * Input.ModifierRatio + 1);

            Parameters.ScaleRatio = Controller.CurrentMode == EditController.SelectionMode.X ? new Vector3(offset.X, 1, 1)
                                  : Controller.CurrentMode == EditController.SelectionMode.Y ? new Vector3(1, offset.Y, 1)
                                  : new Vector3(offset.X, offset.Y, 1);
        }

        public override void Initialize()
        {
            base.Initialize();
            Parameters.ScaleCenter = CenterPos;
        }
    }
}
