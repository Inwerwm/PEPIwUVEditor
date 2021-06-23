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

        private MovingController Controller { get; }
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
            Controller = new MovingController(device, Process.Effect, Process.Rasterize.Solid)
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
            ;
        }

        protected override SelectionMode CalcMode(InputStates input)
        {
            return SelectionMode.None;
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
