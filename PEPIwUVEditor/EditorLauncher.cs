using DxManager;
using DxManager.Camera;
using IwUVEditor.DirectX;
using IwUVEditor.StateContainer;
using PEPlugin;
using SlimDX;
using System;

namespace IwUVEditor
{
    class EditorLauncher : IDisposable
    {
        private bool isDrawing;
        private bool disposedValue;

        Editor Editor { get; }
        FormEditor Form { get; }
        DxContext DrawContext { get; }
        UVViewDrawProcess DrawProcess { get; set; }

        public EditorStates Current { get; }

        float CenterPosition => 0;
        float Zoom => 1;

        public EditorLauncher(IPERunArgs args)
        {
            // インスタンスの作成
            Current = new EditorStates();
            Editor = new Editor(args, Current);
            Form = Form ?? new FormEditor(Editor, Current);
            DrawContext = DrawContext ?? new DxContext(Form.DrawTargetControl)
            {
                RefreshRate = 120
            };

            // Editorへの入力
            Editor.ToolBox.Device = DrawContext.Device;
            // Formへの入力
            Form.DrawContext = DrawContext;
            Form.AddProcessWhenClosing((sender, e) =>
            {
                e.Cancel = true;
                StopDraw();
            });

            // フィールドの初期値を明示
            isDrawing = false;
        }

        public void Run()
        {
            StopDraw();
            Editor.LoadModel();
            Form.LoadMaterials(Editor.Materials.ToArray());
            StartDraw();
            Form.InitializeWhenStartDrawing();
        }

        /// <summary>
        /// DrawProcessを新規に作り描画ループに追加する
        /// </summary>
        public void StartDraw()
        {
            if (isDrawing)
                StopDraw();
            isDrawing = true;
            CreateDrawProcess();
            DrawContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
            Form.Visible = true;
        }

        /// <summary>
        /// 描画プロセスをループから除去しフォームを非表示にする
        /// </summary>
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

            DrawProcess.CatchException += (ex) =>
            {
                StopDraw();

                PEPExtensions.Utility.ShowException(ex);
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    Editor?.Dispose();
                    DrawProcess?.Dispose();
                    DrawContext?.Dispose();
                    Form?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~EditorLauncher()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
