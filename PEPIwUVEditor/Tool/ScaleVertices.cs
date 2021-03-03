using IwUVEditor.DirectX;
using IwUVEditor.DirectX.DrawElement;
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
        SlimDX.Direct3D11.Device Device { get; }

        ScalingController Controller { get; }
        Vector2 ScreenSize { get; set; }

        protected override Matrix Offset => Matrix.Identity;

        public ScaleVertices(SlimDX.Direct3D11.Device device, UVViewDrawProcess process) : base(process)
        {
            Device = device;

            Controller = new ScalingController(Device, Process.Effect, Process.Rasterize.Solid, 0.1f, Process.ScreenSize);
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

    }
}
