﻿using IwUVEditor.DirectX;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.StateContainer;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Controller
{
    class ScaleController : EditController
    {
        private bool disposedValue;

        private ScalingControllerPolygons Controller { get; }
        public override Vector3 Center
        {
            get => Parameters.ScaleCenter;
            set => Parameters.ScaleCenter = value;
        }

        public ScaleController(UVViewDrawProcess process, SlimDX.Direct3D11.Device device, Tool.IEditParameter parameters) : base(process, parameters)
        {
            Controller = new ScalingControllerPolygons(
                device,
                Process.Effect,
                Process.Rasterize.Solid,
                0.1f,
                Process.ScreenSize
            );

            Process.ScreenSizeChanged += Process_ScreenSizeChanged;
            Parameters.ScaleCenterChanged += Parameters_ScaleCenterChanged;
        }

        private void Parameters_ScaleCenterChanged(Vector3 value)
        {
            Controller.Center = value;
        }

        private void Process_ScreenSizeChanged(Vector2 screenSize)
        {
            Controller.ScreenSize = screenSize;
        }

        public override void PrepareDrawing()
        {
            Controller.Prepare();
        }

        protected override void ApplyModeChange(SelectionMode mode)
        {
            Controller.SelectedElement = CurrentMode == SelectionMode.Center ? ScalingControllerPolygons.Elements.Center
                           : CurrentMode == SelectionMode.X ? ScalingControllerPolygons.Elements.X
                           : CurrentMode == SelectionMode.Y ? ScalingControllerPolygons.Elements.Y
                           : ScalingControllerPolygons.Elements.None;
        }

        protected override SelectionMode CalcMode(InputStates input)
        {
            var relationalMousePos = input.MousePos.ElementDivision(Process.ScreenSize);

            Vector2 centerOnScreen = Process.WorldPosToScreenPos(Center.ToVector2());
            var centerOffset = centerOnScreen.ElementDivision(Process.ScreenSize).ToVector3();

            var cSq = Controller.CenterSquareCoord.ToRectangleF(centerOffset, cd => cd / 2);
            var xSq = Controller.XSquareCoord.ToRectangleF(centerOffset, cd => cd / 2);
            var ySq = Controller.YSquareCoord.ReverseY().ToRectangleF(centerOffset, cd => cd / 2);

            return cSq.Contains(relationalMousePos.X, relationalMousePos.Y) ? SelectionMode.Center :
                   xSq.Contains(relationalMousePos.X, relationalMousePos.Y) ? SelectionMode.X :
                   ySq.Contains(relationalMousePos.X, relationalMousePos.Y) ? SelectionMode.Y :
                                                                              SelectionMode.None;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Controller.Dispose();
                    Process.ScreenSizeChanged -= Process_ScreenSizeChanged;
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
