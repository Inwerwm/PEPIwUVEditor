﻿using IwUVEditor.Command;
using IwUVEditor.Manager;
using IwUVEditor.Tool;
using SlimDX;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor.StateContainer
{
    class EditorStates
    {
        private float radiusOfPositionSquare;

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
