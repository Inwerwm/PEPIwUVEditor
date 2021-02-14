using DxManager;
using DxManager.Camera;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class ViewControl
    {
        private bool isDrawing;

        public InputManager Current { get; }

        FormEditor Form { get; }

        DxContext DrawContext { get; }
        UVViewDrawProcess DrawProcess { get; set; }

        float CenterPosition => 0;
        float Zoom => 1;


        public ViewControl(InputManager inputManager)
        {
            Current = inputManager;

            Form = Form ?? new FormEditor(Current);
            DrawContext = DrawContext ?? new DxContext(Form.DrawTargetControl);
            isDrawing = false;
        }

        public void StartDraw()
        {
            if (isDrawing)
                StopDraw();
            CreateDrawProcess();
            DrawContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
            isDrawing = true;
        }

        public void StopDraw()
        {
            if (!isDrawing)
                return;
            DrawContext.StopDrawLoop();
            isDrawing = false;
        }

        void CreateDrawProcess()
        {
            DrawProcess?.Dispose();
            DrawProcess = new UVViewDrawProcess(Current)
            {
                Camera = new DxCameraOrthographic()
                {
                    Position = new Vector3(CenterPosition, CenterPosition, -1),
                    Target = new Vector3(CenterPosition, CenterPosition, 0),
                    Up = new Vector3(0, -1, 0),
                    ViewVolumeSize = (DrawContext.TargetControl.ClientSize.Width / Zoom, DrawContext.TargetControl.ClientSize.Height / Zoom),
                    ViewVolumeDepth = (0, 1)
                },
                RadiusOfPositionSquare = Form.RadiusOfPositionSquare,
                ColorInDefault = new Color4(1, 0, 0, 0),
                ColorInSelected = new Color4(1, 1, 0, 0)
            };
        }

    }
}
