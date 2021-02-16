using IwUVEditor.Command;
using IwUVEditor.Manager;
using IwUVEditor.Tool;
using SlimDX;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor.StateContainer
{
    class EditorStates
    {
        public Material Material { get; set; }
        public IEditTool Tool { get; set; }
    }
}
