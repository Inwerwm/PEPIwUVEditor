using IwUVEditor.Command;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public EditorStates Current { get; }

        // エディタ機能
        public Tool.ToolBox ToolBox { get; }
        Dictionary<Material, CommandManager> Commanders { get; set; }
        SlimDX.Vector2? PositionClip { get; set; }

        // 描画の更新メソッド
        public Action UpdateDraw { get; set; }
        // エディタのリセットメソッド
        Action Resetter { get; }

        public Editor(IPERunArgs args, EditorStates inputManager, Action resetter)
        {
            Args = args;
            Current = inputManager;
            ToolBox = new Tool.ToolBox();
            Resetter = resetter;
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            if (!Pmx.Material.Any())
                throw new Exception("モデルに材質が含まれていません。");
            Materials = Pmx.Material.Select((material, i) => new Material(material, Pmx)).ToList();
            Commanders = Materials.ToDictionary(m => m, _ => new CommandManager());
            Current.Material = Materials.First();
        }

        public void SendModel()
        {
            PEPExtensions.Utility.Update(Args.Host.Connector, Pmx);
        }

        public void Reset()
        {
            Resetter.Invoke();
        }

        public void DriveTool(InputStates input)
        {
            if (Current.Tool is null)
                return;

            Current.Tool.ReadInput(input);
            if (Current.Tool.IsReady)
                Do(Current.Material, Current.Tool.CreateCommand(Current.Material));
        }

        public void Do(Material targetMaterial, IEditorCommand command)
        {
            if (targetMaterial is null)
                return;

            Commanders[targetMaterial].Do(command);
            UpdateDraw();
        }

        public void Undo()
        {
            if (Current.Material is null)
                return;

            Commanders[Current.Material].Undo();
            UpdateDraw();
        }

        public void Redo()
        {
            if (Current.Material is null)
                return;

            Commanders[Current.Material].Redo();
            UpdateDraw();
        }

        public void CopyPosition()
        {
            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value).Select(v => v.Key);

            //LinqのAverageメソッドだと2回回す必要があるのでforeachで平均を計算する
            PEPlugin.SDX.V2 sum = new PEPlugin.SDX.V2(0, 0);
            int count = 0;
            foreach (var vtx in selectedVertices)
            {
                sum += vtx.UV;
                count++;
            }

            PositionClip = sum / count;
        }

        public void PastePosition()
        {
            if (PositionClip is null)
                return;

            var selectedVertices = Current.Material.IsSelected.Where(isSelected => isSelected.Value).Select(v => v.Key).ToList();
            Do(Current.Material, new CommandSetPosition(selectedVertices, PositionClip.Value));
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
