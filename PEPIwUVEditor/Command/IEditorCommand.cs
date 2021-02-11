using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    interface IEditorCommand
    {
        void Do();
        void Undo();
    }
}
