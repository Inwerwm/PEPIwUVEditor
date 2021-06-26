using IwUVEditor.StateContainer;
using SlimDX;
using System;

namespace IwUVEditor.Controller
{
    class RotateController : EditController
    {
        private bool disposedValue;
        private float radius = 0.025f;

        private DirectX.DrawElement.RotationCenterSign CenterSign { get; set; }
        public override Vector3 Center
        {
            get => Parameters.RotationCenter;
            set => Parameters.RotationCenter = value;
        }
        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                CenterSign.Radius = value;
            }
        }
        public RotateController(DirectX.UVViewDrawProcess process, SlimDX.Direct3D11.Device device, Tool.IEditParameter parameters) : base(process, parameters)
        {
            CenterSign = new DirectX.DrawElement.RotationCenterSign(
                device,
                Process.Effect,
                Process.Rasterize.Solid,
                Center,
                Radius,
                new Color4(0, 0, 1),
                Process.ScreenSize
            );

            Process.ScreenSizeChanged += Process_ScreenSizeChanged;
            Parameters.RotationCenterChanged += Parameters_RotationCenterChanged;
        }

        private void Parameters_RotationCenterChanged(Vector3 value)
        {
            CenterSign.Center = value;
        }

        private void Process_ScreenSizeChanged(Vector2 screenSize)
        {
            CenterSign.ScreenSize = screenSize;
        }

        public override void PrepareDrawing()
        {
            CenterSign.Prepare();
        }

        protected override void ApplyModeChange(SelectionMode mode)
        {
            CenterSign.Color = mode == SelectionMode.Center ? new Color4(1, 0, 1) : new Color4(0, 0, 1);
        }

        protected override SelectionMode CalcMode(InputStates input)
        {
            var mousePos = input.MousePos;
            var vecOfRotationCenterToMouseCursor = mousePos - Process.WorldPosToScreenPos(new Vector2(Center.X, Center.Y));
            var normalizedRelationalCursorPosition = vecOfRotationCenterToMouseCursor.ElementDivision(Process.ScreenSize);

            // 正規化された相対的カーソル位置は [0,1] の値域で計算されている
            // なので [-1,1] の値域での長さは 2 倍された値になる
            float distanceFromRotationCenter = normalizedRelationalCursorPosition.Length() * 2;

            return distanceFromRotationCenter < Radius ? SelectionMode.Center : SelectionMode.None;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CenterSign?.Dispose();
                    Process.ScreenSizeChanged -= Process_ScreenSizeChanged;
                    Parameters.RotationCenterChanged -= Parameters_RotationCenterChanged;
                }
                disposedValue = true;
            }
        }

        public override void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
