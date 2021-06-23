using IwUVEditor.DirectX;
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
    class MoveController : EditController, IDisposable
    {
        private bool disposedValue;
        private Vector3 center;

        private MovingControllerPolygons Controller { get; }
        public override Vector3 Center
        {
            get => center;
            set
            {
                center = value;
                Controller.Center = value;
            }
        }

        public MoveController(UVViewDrawProcess process, SlimDX.Direct3D11.Device device) : base(process)
        {
            Controller = new MovingControllerPolygons(device, Process.Effect, Process.Rasterize.Solid)
            {
                Center = Center,
                ScreenSize = Process.ScreenSize
            };

            Process.ScreenSizeChanged += Process_ScreenSizeChanged;
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
            Controller.SelectedElement = mode == SelectionMode.X ? MovingControllerPolygons.Elements.X
                                       : mode == SelectionMode.Y ? MovingControllerPolygons.Elements.Y
                                       : MovingControllerPolygons.Elements.None;
        }

        protected override SelectionMode CalcMode(InputStates input)
        {
            var mouseScreenPos = input.MousePos;
            var centerScreenPos = Process.WorldPosToScreenPos(Center.ToVector2());

            var mouseOffset = mouseScreenPos.ElementDivision(Process.ScreenSize).ToVector3();
            var centerOffset = centerScreenPos.ElementDivision(Process.ScreenSize).ToVector3();

            // 三角形の座標を2で割ることで
            // [-1,1]範囲での移動量表現を [0,1]範囲での移動量表現に変える
            // 描画時は上下が反転するためY端子は上下反転する
            var xHead = (Controller.XAxisHeadVertices / 2).Shift(centerOffset);
            var yHead = (Controller.YAxisHeadVertices / 2).ReverseY().Shift(centerOffset);

            return xHead.Include(mouseOffset) ? SelectionMode.X
                 : yHead.Include(mouseOffset) ? SelectionMode.Y
                 : SelectionMode.None;
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

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
