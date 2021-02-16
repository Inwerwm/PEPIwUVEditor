using IwUVEditor.Command;
using IwUVEditor.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    class RectangleSelection : IEditTool
    {
        public bool IsReady => throw new NotImplementedException();

        public IEditorCommand CreateCommand()
        {
            if (!IsReady)
                throw new InvalidOperationException();
        }

        public void ReadMouse(DragManager mouse)
        {
            throw new NotImplementedException();
        }
    }
}
