﻿using System.Collections.Generic;
using System.Linq;

namespace IwUVEditor.Command
{
    class CommandManager
    {
        private Stack<IEditorCommand> UndoStack { get; }
        private Stack<IEditorCommand> RedoStack { get; }

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
    }
}
