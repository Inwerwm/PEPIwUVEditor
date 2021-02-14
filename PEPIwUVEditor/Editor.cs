using IwUVEditor.Command;
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
        // モデル関連プロパティ
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        public List<Material> Materials { get; private set; }
        public Material CurrentMaterial { get; set; }
        public Tool CurrentTool { get; set; }

        // エディタ機能関連プロパティ
        public Tools Tools { get; private set; }

        public Editor(IPERunArgs args)
        {
            Args = args;
        }

        public void Undo()
        {
            if (CurrentMaterial is null)
                return;

            Tools.Undo(CurrentMaterial);
        }

        public void Redo()
        {
            if (CurrentMaterial is null)
                return;

            Tools.Redo(CurrentMaterial);
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            Materials = Pmx.Material.Select((material, i) => new Material(material, Pmx)).ToList();

            Tools = new Tools(Materials);
        }
    }
}
