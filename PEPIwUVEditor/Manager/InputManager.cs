using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Manager
{
    class InputManager
    {
        public DragManager MouseLeft { get; }

        public Material Material { get; set; }

        public Tool Tool { get; set; }
    }
}
