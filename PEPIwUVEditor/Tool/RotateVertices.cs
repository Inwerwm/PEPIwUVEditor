﻿using IwUVEditor.DirectX;
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
        private RotationCenterSign CenterSign { get; set; }

        SlimDX.Direct3D11.Device Device { get; }

        Vector2 ScreenSize { get; set; }

        /// <summary>
        /// 回転量/移動量
        /// </summary>
        private static double Step => 0.001;
        protected override Matrix Offset
        {
            get
            {
                return Matrix.Translation(CenterPos * -1) * Matrix.RotationZ((float)(Step * Input.MouseOffset.Y * 2 * Math.PI)) * Matrix.Translation(CenterPos);
            }
        }

        public RotateVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process)
        {
            Device = device;
            CenterSign = new RotationCenterSign(
                Device,
                Process.Effect,
                Process.Rasterize.Solid,
                CenterPos,
                0.1f,
                new Color4(0, 0, 1),
                ScreenSize
            );
        }

        public override void Initialize()
        {
            base.Initialize();
            CenterSign.ScreenSize = Process.ScreenSize;
            ScreenSize = Process.ScreenSize;
        }

        public override void PrepareDrawing()
        {
            base.PrepareDrawing();
            CenterSign.Prepare();
        }

        public override void ReadInput(InputStates input)
        {
            Input = input;
            if(ScreenSize != Process.ScreenSize)
            {
                CenterSign.ScreenSize = Process.ScreenSize;
                ScreenSize = Process.ScreenSize;
            }

            base.ReadInput(input);
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
