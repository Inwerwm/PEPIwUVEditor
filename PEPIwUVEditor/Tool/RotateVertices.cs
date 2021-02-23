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
            var radPos = input.MousePos - Process.WorldPosToScreenPos(new Vector2(CenterPos.X, CenterPos.Y));
            var normalizedPos = new Vector2(radPos.X / Process.ScreenSize.X, radPos.Y / Process.ScreenSize.Y);
            float length = normalizedPos.Length() * 2;
            // TODO: 回転中心選択状態と回転操作状態がドラッグ中に遷移すると起きる不具合が起きるので修正する
            if (length < Radius)
            {
                CenterSign.Color = new Color4(1, 0, 1);
            }
            else
            {
                CenterSign.Color = new Color4(0, 0, 1);
                base.ReadInput(input);
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
    }
}
