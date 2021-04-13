using IwUVEditor.DirectX;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.StateContainer;
using SlimDX;

namespace IwUVEditor.Tool
{
    class ScaleVertices : EditVertices, IEditTool
    {
        private Vector3 scalingCenter;

        SlimDX.Direct3D11.Device Device { get; }

        ScalingController Controller { get; }
        Vector2 ScreenSize { get; set; }

        Vector3 ScalingCenter
        {
            get => scalingCenter;
            set
            {
                scalingCenter = value;
                Controller.Center = scalingCenter;
            }
        }

        private static float Step => 0.01f;
        Mode CurrentMode { get; set; }
        Matrix CurrentScale
        {
            get
            {
                switch (CurrentMode)
                {
                    case Mode.X:
                        return XScale;
                    case Mode.Y:
                        return YScale;
                    default:
                        return BothScale;
                }
            }
        }

        private Matrix BothScale => Matrix.Scaling(new Vector3(1 + Input.MouseOffset.X * Step, 1 - Input.MouseOffset.Y * Step, 1));
        private Matrix XScale => Matrix.Scaling(new Vector3(1 + Input.MouseOffset.X * Step, 1, 1));
        private Matrix YScale => Matrix.Scaling(new Vector3(1, 1 - Input.MouseOffset.Y * Step, 1));

        protected override Matrix Offset => Matrix.Invert(Matrix.Translation(ScalingCenter)) * CurrentScale * Matrix.Translation(ScalingCenter);

        public ScaleVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process)
        {
            Device = device;

            Controller = new ScalingController(Device, Process.Effect, Process.Rasterize.Solid, 0.1f, Process.ScreenSize);
        }

        public override void Initialize()
        {
            base.Initialize();
            ScalingCenter = CenterPos;
        }

        public override void PrepareDrawing()
        {
            base.PrepareDrawing();
            if (ScreenSize != Process.ScreenSize)
            {
                Controller.ScreenSize = Process.ScreenSize;
                ScreenSize = Process.ScreenSize;
            }
            Controller.Prepare();
        }

        public override void ReadInput(InputStates input)
        {

            var modeTmp = CurrentMode;

            if (!(input.MouseLeft.IsDragging || input.MouseLeft.IsEndingJust))
                CurrentMode = checkMode(input.MousePos);

            switch (CurrentMode)
            {
                case Mode.MoveCenter:
                    MoveCenter(input);
                    break;
                default:
                    base.ReadInput(input);
                    break;
            }

            if (input.MouseLeft.IsEndingJust)
                CurrentMode = checkMode(input.MousePos);

            if (modeTmp != CurrentMode)
                Controller.SelectedElement = CurrentMode == Mode.MoveCenter ? ScalingController.Elements.Center
                                           : CurrentMode == Mode.X ? ScalingController.Elements.X
                                           : CurrentMode == Mode.Y ? ScalingController.Elements.Y
                                           : ScalingController.Elements.None;

            Mode checkMode(Vector2 mousePos)
            {
                var rationalMousePos = mousePos.ElementDivision(Process.ScreenSize);

                Vector2 centerOnScreen = Process.WorldPosToScreenPos(ScalingCenter.ToVector2());
                var centerOffset = centerOnScreen.ElementDivision(Process.ScreenSize).ToVector3();

                var cSq = Controller.CenterSquareCoord.ToRectangleF(centerOffset, cd => cd / 2);
                var xSq = Controller.XSquareCoord.ToRectangleF(centerOffset, cd => cd / 2);
                var ySq = Controller.YSquareCoord.ReverseY().ToRectangleF(centerOffset, cd => cd / 2);

                return cSq.Contains(rationalMousePos.X, rationalMousePos.Y) ? Mode.MoveCenter :
                       xSq.Contains(rationalMousePos.X, rationalMousePos.Y) ? Mode.X :
                       ySq.Contains(rationalMousePos.X, rationalMousePos.Y) ? Mode.Y :
                                                                              Mode.Both;
            }
        }

        private void MoveCenter(InputStates input)
        {
            if (input.MouseLeft.IsDragging)
            {
                ScalingCenter += new Vector3(input.MouseLeft.Offset, 0);
            }
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

        enum Mode
        {
            Both,
            X,
            Y,
            MoveCenter
        }
    }
}
