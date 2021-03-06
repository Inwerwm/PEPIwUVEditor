﻿using IwUVEditor.Controller;
using IwUVEditor.DirectX;
using SlimDX;
using System;

namespace IwUVEditor.Tool
{
    class RotateVertices : EditVertices, IEditTool
    {
        /// <summary>
        /// 回転量/移動量
        /// </summary>
        private static double Step => 0.001;
        protected override Matrix Offset =>
            Matrix.Translation(Parameters.RotationCenter * -1) * Matrix.RotationZ(Parameters.RotationAngle) * Matrix.Translation(Parameters.RotationCenter);

        public RotateVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process, IEditParameter parameters) : base(process, new RotateController(process, device, parameters), parameters) { }

        protected override void UpdateParameter()
        {
            Parameters.RotationAngle = (float)(Step * Input.MouseOffset.Y * 2 * Math.PI);
        }

        public override void Initialize()
        {
            base.Initialize();
            Parameters.RotationCenter = CenterPos;
        }
    }
}
