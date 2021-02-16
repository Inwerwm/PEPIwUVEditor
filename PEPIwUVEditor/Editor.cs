using DxManager;
using DxManager.Camera;
using IwUVEditor.Command;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using PEPlugin;
using PEPlugin.Pmx;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    /// <summary>
    /// エディタ機能クラス
    /// </summary>
    class Editor : IDisposable
    {
        private bool disposedValue;

        // モデル
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        public List<Material> Materials { get; private set; }

        // 現在の状態
        public InputManager Current { get; }

        // エディタ機能
        public Tool.ToolBox ToolBox { get; }
        Dictionary<Material, CommandManager> Commanders { get; set; }

        public Editor(IPERunArgs args, InputManager inputManager)
        {
            Args = args;
            Current = inputManager;
            ToolBox = new Tool.ToolBox();

            Current.InvokeCommand += (command) =>
            {
                Commanders[Current.Material].Do(command);
            };
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            Materials = Pmx.Material.Select((material, i) => new Material(material, Pmx)).ToList();
            Commanders = Materials.ToDictionary(m => m, _ => new CommandManager());
        }

        public void DriveTool(DragManager mouse, Dictionary<System.Windows.Forms.Keys, bool> pressKey)
        {
            Current.Tool.ReadInput(mouse, pressKey);
            if (Current.Tool.IsReady)
                Commanders[Current.Material].Do(Current.Tool.CreateCommand(Current.Material));
        }

        public void Undo()
        {
            if (Current.Material is null)
                return;

            Commanders[Current.Material].Undo();
        }

        public void Redo()
        {
            if (Current.Material is null)
                return;

            Commanders[Current.Material].Redo();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    ToolBox?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~Editor()
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
