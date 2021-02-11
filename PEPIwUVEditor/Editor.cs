using IwUVEditor.Command;
using PEPlugin;
using PEPlugin.Pmx;
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
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        public IEnumerable<Material> Materials { get; private set; }

        private Stack<IEditorCommand> UndoStack { get; }
        private Stack<IEditorCommand> RedoStack { get; }

        public Editor(IPERunArgs args)
        {
            Args = args;

            LoadModel();
        }

        public void Do(IEditorCommand cmd)
        {
            cmd.Do();
            UndoStack.Push(cmd);
            RedoStack.Clear();
        }

        /// <summary>
        /// 元に戻す
        /// </summary>
        public void Undo()
        {
            if (!UndoStack.Any())
                return;

            var com = UndoStack.Pop();
            com.Undo();
            RedoStack.Push(com);
        }

        /// <summary>
        /// やり直し
        /// </summary>
        public void Redo()
        {
            if (!RedoStack.Any())
                return;

            var com = RedoStack.Pop();
            com.Do();
            UndoStack.Push(com);
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
