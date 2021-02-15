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
    class Editor
    {
        // モデル
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        public List<Material> Materials { get; private set; }

        // 現在の状態
        public InputManager Current { get; }

        // エディタ機能
        Dictionary<Material, CommandManager> Commanders { get; set; }

        public Editor(IPERunArgs args)
        {
            Args = args;
            Current = new InputManager();

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
    }
}
