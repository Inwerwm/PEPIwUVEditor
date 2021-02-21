using IwUVEditor.Manager;
using SlimDX;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor.StateContainer
{
    class InputStates
    {
        public bool IsActive { get; set; }
        /// <summary>
        /// マウスのスクリーン座標
        /// </summary>
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
