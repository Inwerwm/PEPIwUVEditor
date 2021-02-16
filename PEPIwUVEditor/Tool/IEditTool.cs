using IwUVEditor.Command;
using IwUVEditor.Manager;
using System;
using System.Collections.Generic;

namespace IwUVEditor.Tool
{
    interface IEditTool : IDisposable
    {
        bool IsReady { get; }

        void ReadInput(DragManager mouse, Dictionary<System.Windows.Forms.Keys, bool> pressKey);
        IEditorCommand CreateCommand(Material target);
        void PrepareDrawing();
    }
}
