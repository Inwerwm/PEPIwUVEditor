using IwUVEditor.Command;
using System.Collections.Generic;
using System.Linq;

namespace IwUVEditor.Manager
{
    class CommandManager
    {
        private Stack<IEditorCommand> UndoStack { get; }
        private Stack<IEditorCommand> RedoStack { get; }

        private uint EditCount { get; set; }
        public bool IsEdited => EditCount > 0;

        public CommandManager()
        {
            UndoStack = new Stack<IEditorCommand>();
            RedoStack = new Stack<IEditorCommand>();
        }

        public void Do(IEditorCommand cmd)
        {
            cmd.Do();
            UndoStack.Push(cmd);
            RedoStack.Clear();

            if (cmd.IsDestructive)
                EditCount++;
        }

        /// <summary>
        /// 元に戻す
        /// </summary>
        /// <returns>破壊的命令をUndoしたか</returns>
        public bool Undo()
        {
            if (!UndoStack.Any())
                return false;

            var cmd = UndoStack.Pop();
            cmd.Undo();
            RedoStack.Push(cmd);

            if (cmd.IsDestructive)
                EditCount--;

            return cmd.IsDestructive;
        }

        /// <summary>
        /// やり直し
        /// </summary>
        public void Redo()
        {
            if (!RedoStack.Any())
                return;

            var cmd = RedoStack.Pop();
            cmd.Do();
            UndoStack.Push(cmd);

            if (cmd.IsDestructive)
                EditCount++;
        }
    }
}
