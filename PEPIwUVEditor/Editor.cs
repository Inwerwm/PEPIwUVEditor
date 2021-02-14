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

        // 描画
        ViewControl ViewControl { get; }

        // 現在の状態
        public InputManager Current { get; }

        // エディタ機能関連プロパティ
        public Tools Tools { get; private set; }

        public Editor(IPERunArgs args)
        {
            Args = args;
            ViewControl = new ViewControl(Current);
            Current = new InputManager();
        }

        public void Run()
        {
            ViewControl.StopDraw();
            LoadModel();
            ViewControl.StartDraw();
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            Materials = Pmx.Material.Select((material, i) => new Material(material, Pmx)).ToList();
            Tools = new Tools(Materials);
        }

        public void Undo()
        {
            if (Current.Material is null)
                return;

            Tools.Undo(Current.Material);
        }

        public void Redo()
        {
            if (Current.Material is null)
                return;

            Tools.Redo(Current.Material);
        }
    }
}
