using IwUVEditor.Tool;

namespace IwUVEditor.StateContainer
{
    class EditorStates
    {
        private IEditTool tool;

        public Material Material { get; set; }
        public IEditTool Tool
        {
            get => tool;
            set
            {
                tool = value;
                tool.Initialize();
            }
        }
    }
}
