namespace IwUVEditor.Command
{
    interface IEditorCommand
    {
        /// <summary>
        /// このコマンドは頂点に対し変更を加えるか？
        /// </summary>
        bool IsDestructive { get; }
        void Do();
        void Undo();
    }
}
