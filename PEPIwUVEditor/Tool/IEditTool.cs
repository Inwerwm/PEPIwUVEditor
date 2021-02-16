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

        void ReadMouse(DragManager mouse);
        IEditorCommand CreateCommand();
    }
}
