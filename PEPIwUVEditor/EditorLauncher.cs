using DxManager;
using DxManager.Camera;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using PEPlugin;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class EditorLauncher
    {
        private bool isDrawing;

        Editor Editor { get; }
        FormEditor Form { get; }
        DxContext DrawContext { get; }
        UVViewDrawProcess DrawProcess { get; set; }

        public InputManager Current { get; }

        float CenterPosition => 0;
        float Zoom => 1;

        public EditorLauncher(IPERunArgs args)
        {
            Current = new InputManager();
            Editor = new Editor(args, Current);
            Form = Form ?? new FormEditor(Editor, Current);
            DrawContext = DrawContext ?? new DxContext(Form.DrawTargetControl)
            {
                RefreshRate = 120
            };
            Form.DrawContext = DrawContext;

            isDrawing = false;
        }

        public void Run()
        {
            StopDraw();
            Editor.LoadModel();
            LoadMaterials(Editor.Materials);
            StartDraw();
        }

        public void StartDraw()
        {
            if (isDrawing)
                StopDraw();
            isDrawing = true;
            CreateDrawProcess();
            DrawContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
            Form.Visible = true;
        }

        public void StopDraw()
        {
            if (!isDrawing)
                return;

            Form.Visible = false;
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
                ColorInDefault = new Color4(1, 0, 0, 0),
                ColorInSelected = new Color4(1, 1, 0, 0)
            };
            Form.DrawProcess = DrawProcess;
        }

        internal void LoadMaterials(List<Material> materials)
        {
            Form.LoadMaterials(materials.ToArray());
        }

        internal void ChangeScreenSize()
        {
            DrawContext?.ChangeResolution();
            DrawProcess?.ChangeResolution();
        }

        internal void ResetCamera()
        {
            DrawProcess.ResetCamera();
        }

        internal void ChangeRefreshLimitTo(bool value)
        {
            DrawProcess.LimitRefresh = value;
        }

        internal void OrderUndo()
        {
            Editor.Undo();
        }

        internal void OrderRedo()
        {
            Editor.Redo();
        }
    }
}
