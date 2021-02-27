using IwUVEditor.Command;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using System;
using System.Collections.Generic;

namespace IwUVEditor.Tool
{
    interface IEditTool : IDisposable
    {
        bool IsReady { get; }

        void Initialize();
        void ReadInput(InputStates input);
        IEditorCommand CreateCommand(Material target);
        void PrepareDrawing();
    }
}
