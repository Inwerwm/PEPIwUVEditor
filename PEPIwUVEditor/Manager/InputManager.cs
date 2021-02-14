using IwUVEditor.Command;
using SlimDX;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor.Manager
{
    class InputManager
    {
        private Material material;
        private float radiusOfPositionSquare;

        public DragManager MouseLeft { get; } = new DragManager();
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };

        public Vector2 ScreenSize { get; set; }
        public Vector2 MousePos { get; set; }

        public bool IsActive { get; set; }

        public Material Material
        {
            get => material;
            set
            {
                material = value;
                if (!(MaterialIsChanged is null))
                    MaterialIsChanged(material);
            }
        }
        public Tool Tool { get; set; }

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

        public IEditorCommand Command
        {
            set
            {
                if (Material is null)
                    return;
                InvokeCommand(value);
            }
        }

        public event InputSetEventHandler MaterialIsChanged;
        public event InputSetEventHandler RadiusOfPosSqIsChanged;

        public event CommandInvokeHandler InvokeCommand;
    }

    delegate void InputSetEventHandler(object setValue);
    delegate void CommandInvokeHandler(IEditorCommand command);
}
