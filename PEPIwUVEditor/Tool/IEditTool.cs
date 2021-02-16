using IwUVEditor.Command;
using IwUVEditor.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    interface IEditTool
    {
        bool IsReady { get; }

        void ReadInput(DragManager mouse, Dictionary<System.Windows.Forms.Keys, bool> pressKey);
        IEditorCommand CreateCommand(Material target);
        void PrepareDrawing();
    }
}
