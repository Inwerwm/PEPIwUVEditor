using IwUVEditor.DirectX;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.StateContainer;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        Mode CurrentMode { get; set; }
        Matrix PreviousScale { get; set; } = Matrix.Identity;
        Matrix CurrentScale => Matrix.Scaling(new Vector3(Input.MouseLeft.Translation, 1));
        protected override Matrix Offset => Matrix.Invert(PreviousScale) * Matrix.Invert(Matrix.Translation(ScalingCenter)) * CurrentScale * Matrix.Translation(ScalingCenter);

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

            //switch (CurrentMode)
            //{
            //    case Mode.MoveCenter:
            //        MoveCenter(input);
            //        break;
            //    default:
            //        base.ReadInput(input);
            //        break;
            //}

            if (input.MouseLeft.IsEndingJust)
                CurrentMode = checkMode(input.MousePos);

            if (modeTmp != CurrentMode)
                Controller.SelectedElement = CurrentMode == Mode.MoveCenter ? ScalingController.Elements.Center
                                           : CurrentMode == Mode.X ?          ScalingController.Elements.X
                                           : CurrentMode == Mode.Y ?          ScalingController.Elements.Y
                                           :                                  ScalingController.Elements.None
                                           ;

            //PreviousScale = CurrentScale;

            Mode checkMode(Vector2 mousePos)
            {
                var rationalMousePos = mousePos.ElementDivision(Process.ScreenSize);

                Vector2 centerOnScreen = Process.WorldPosToScreenPos(ScalingCenter.ToVector2());
                var centerOffset = centerOnScreen.ElementDivision(Process.ScreenSize).ToVector3();

                var cSq = Controller.CenterSquareCoord.ToRectangleF(centerOffset, cd => cd / 2);
                var xSq = Controller.XSquareCoord.ToRectangleF(centerOffset, cd => cd / 2);
                var ySq = Controller.YSquareCoord.ReverseY().ToRectangleF(centerOffset, cd => cd / 2);

                Log.DebugLog.Write(
                    $"Log : {DateTime.Now:HH:mm:ss.ff}{Environment.NewLine}" +
                    $"{Environment.NewLine}" +
                    $"カーソルのスクリーン位置 :\t{mousePos}{Environment.NewLine}" +
                    $"カーソルの割合での位置 :\t{rationalMousePos}{Environment.NewLine}" +
                    $"{Environment.NewLine}" +
                    $"回転中心コントローラ座標 :{Environment.NewLine}" +
                    $"{Controller.CenterSquareCoord}{Environment.NewLine}" +
                    $"中心の判定領域 : {cSq}{Environment.NewLine}" +
                    $"{Environment.NewLine}" +
                    $"X移動コントローラ座標 :{Environment.NewLine}" +
                    $"{Controller.XSquareCoord}{Environment.NewLine}" +
                    $"X移動の判定領域 : {xSq}{Environment.NewLine}" +
                    $"{Environment.NewLine}" +
                    $"Y移動コントローラ座標 :{Environment.NewLine}" +
                    $"{Controller.YSquareCoord}{Environment.NewLine}" +
                    $"Y移動の判定領域 : {ySq}"
                );

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
