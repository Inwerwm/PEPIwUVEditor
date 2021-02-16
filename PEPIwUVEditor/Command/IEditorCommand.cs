namespace IwUVEditor.Command
{
    interface IEditorCommand
    {
        void Do();
        void Undo();
    }
}
