﻿using IwUVEditor.Command;
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
        public IEnumerable<Material> Materials { get; private set; }
        public Tool CurrentTool { get; set; }

        // エディタ機能関連プロパティ
        private CommandManager Commander { get; }
        public Tools Tools { get; }

        public Editor(IPERunArgs args)
        {
            Args = args;

            Commander = new CommandManager();
            Tools = new Tools(Commander);

            LoadModel();
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            Materials = Pmx.Material.Select((material, i) => new Material(material, Pmx));
        }
    }
}
