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
    class RotateVertices : EditVertices, IEditTool
    {
        private Vector3 rotationCenter;

        private RotationCenterSign CenterSign { get; set; }
        private Vector3 RotationCenter
        {
            get => rotationCenter; 
            set
            {
                rotationCenter = value;
                CenterSign.Center = rotationCenter;
            }
        }
        public float Radius { get; set; } = 0.025f;

        SlimDX.Direct3D11.Device Device { get; }

        Vector2 ScreenSize { get; set; }

        /// <summary>
        /// 回転量/移動量
        /// </summary>
        private static double Step => 0.001;
        protected override Matrix Offset =>
            Matrix.Translation(RotationCenter * -1) * Matrix.RotationZ((float)(Step * Input.MouseOffset.Y * 2 * Math.PI)) * Matrix.Translation(RotationCenter);

        private Mode CurrentMode { get; set; }

        public RotateVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process)
        {
            Device = device;

            ScreenSize = Process.ScreenSize;
            CenterSign = new RotationCenterSign(
                Device,
                Process.Effect,
                Process.Rasterize.Solid,
                CenterPos,
                Radius,
                new Color4(0, 0, 1),
                ScreenSize
            );
        }

        public override void Initialize()
        {
            base.Initialize();

            RotationCenter = CenterPos;
        }

        public override void PrepareDrawing()
        {
            base.PrepareDrawing();
            if (ScreenSize != Process.ScreenSize)
            {
                CenterSign.ScreenSize = Process.ScreenSize;
                ScreenSize = Process.ScreenSize;
            }
            CenterSign.Prepare();
        }

        public override void ReadInput(InputStates input)
        {
            var modeTmp = CurrentMode;

            if (!(input.MouseLeft.IsDragging || input.MouseLeft.IsEndingJust))
                CurrentMode = CheckMode(input.MousePos);

            switch (CurrentMode)
            {
                case Mode.Rotation:
                    base.ReadInput(input);
                    break;
                case Mode.MoveCenter:
                    MoveCenter(input);
                    break;
                default:
                    break;
            }

            // ドラッグ終了時点の場合はモードの再読込を行わないと
            // 次のマウス入力までモードが持続してしまう
            if (input.MouseLeft.IsEndingJust)
                CurrentMode = CheckMode(input.MousePos);

            if (modeTmp != CurrentMode)
                CenterSign.Color = CurrentMode == Mode.MoveCenter ? new Color4(1, 0, 1) : new Color4(0, 0, 1);

            Mode CheckMode(Vector2 mousePos)
            {
                var radPos = mousePos - Process.WorldPosToScreenPos(new Vector2(RotationCenter.X, RotationCenter.Y));
                var normalizedPos = new Vector2(radPos.X / Process.ScreenSize.X, radPos.Y / Process.ScreenSize.Y);
                float length = normalizedPos.Length() * 2; // なぜか半分にすると正しそうな長さになる
                return length < Radius ? Mode.MoveCenter : Mode.Rotation;
            }
        }

        private void MoveCenter(InputStates input)
        {
            if (input.MouseLeft.IsDragging)
            {
                RotationCenter += new Vector3(input.MouseLeft.Offset, 0);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CenterSign?.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        enum Mode
        {
            Rotation,
            MoveCenter,
        }
    }
}
