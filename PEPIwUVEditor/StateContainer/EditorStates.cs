using IwUVEditor.Command;
using IwUVEditor.Tool;
using SlimDX;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor.Manager
{
    class EditorStates
    {
        private float radiusOfPositionSquare;

        public DragManager MouseLeft { get; } = new DragManager();
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };

        public Material Material { get; set; }
        public IEditTool Tool { get; set; }

        public float RadiusOfPositionSquare
        {
            get => radiusOfPositionSquare;
            set
            {
                radiusOfPositionSquare = value;
                if (!(RadiusOfPosSqIsChanged is null))
                    RadiusOfPosSqIsChanged(radiusOfPositionSquare);
            }
        }
        public float FPS { get; set; }

        public event InputSetEventHandler RadiusOfPosSqIsChanged;
    }

    delegate void InputSetEventHandler(object setValue);
}
