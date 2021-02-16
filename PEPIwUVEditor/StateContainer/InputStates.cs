using IwUVEditor.Manager;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor.StateContainer
{
    class InputStates
    {
        public bool IsActive { get; set; }
        public Vector2 MousePos { get; set; }
        public DragManager MouseLeft { get; } = new DragManager();
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };
        public Dictionary<MouseButtons, bool> IsClicking { get; } = new Dictionary<MouseButtons, bool>
        {
            { MouseButtons.Left, false },
            { MouseButtons.Middle, false },
            { MouseButtons.Right, false },
        };
    }
}
