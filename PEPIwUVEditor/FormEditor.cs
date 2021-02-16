using DxManager;
using IwUVEditor.Command;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using IwUVEditor.Tool;
using SlimDX;
using SlimDX.RawInput;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor
{
    internal partial class FormEditor : Form
    {
        private UVViewDrawProcess drawProcess;

        Editor Editor { get; }
        internal DxContext DrawContext { get; set; }
        internal UVViewDrawProcess DrawProcess
        {
            get => drawProcess;
            set
            {
                drawProcess = value;
                drawProcess.AddMouseInputProcess(MouseInput);
            }
        }
        EditorStates Current { get; }
        bool IsActive { get; set; }
        Vector2 MousePos { get; set; }

        public Dictionary<MouseButtons, bool> IsClicking { get; } = new Dictionary<MouseButtons, bool>
        {
            { MouseButtons.Left, false },
            { MouseButtons.Middle, false },
            { MouseButtons.Right, false },
        };

        internal Control DrawTargetControl => splitUVMat.Panel1;

        public FormEditor(Editor editor, EditorStates inputManager)
        {
            Editor = editor;
            Current = inputManager;

            InitializeComponent();
            InitializeCurrent();

            timerEvery.Enabled = true;
        }

        private void InitializeCurrent()
        {
            Current.RadiusOfPositionSquare = (float)numericRadiusOfPosSq.Value;
        }

        internal void LoadMaterials(Material[] materials)
        {
            listBoxMaterial.Items.Clear();
            listBoxMaterial.Items.AddRange(materials);
        }

        internal void AddProcessWhenClosing(FormClosingEventHandler handler)
        {
            FormClosing += handler;
        }

        void MouseInput(object sender, MouseInputEventArgs e)
        {
            if (!IsActive)
                return;

            float modifier = (Current.IsPress[Keys.ShiftKey] ? 4f : 1f) / (Current.IsPress[Keys.ControlKey] ? 4f : 1f);

            switch (e.ButtonFlags)
            {
                case MouseButtonFlags.MouseWheel:
                    DrawProcess.Scale.WheelDelta += e.WheelDelta * modifier;
                    break;
                case MouseButtonFlags.MiddleUp:
                    IsClicking[MouseButtons.Middle] = false;
                    break;
                case MouseButtonFlags.MiddleDown:
                    IsClicking[MouseButtons.Middle] = true;
                    break;
                case MouseButtonFlags.RightUp:
                    IsClicking[MouseButtons.Right] = false;
                    break;
                case MouseButtonFlags.RightDown:
                    IsClicking[MouseButtons.Right] = true;
                    break;
                case MouseButtonFlags.LeftUp:
                    IsClicking[MouseButtons.Left] = false;
                    break;
                case MouseButtonFlags.LeftDown:
                    IsClicking[MouseButtons.Left] = true;
                    break;
                default:
                    break;
            }

            if (IsClicking[MouseButtons.Middle])
                DrawProcess.ShiftOffset += modifier * new Vector3(1f * e.X / DrawTargetControl.Width, -1f * e.Y / DrawTargetControl.Height, 0) / DrawProcess.Scale.Scale;

            Current.MouseLeft.ReadState(DrawProcess.ScreenPosToWorldPos(MousePos), IsClicking[MouseButtons.Left]);
            Editor.DriveTool(Current.MouseLeft, Current.IsPress);
        }

        private void splitUVMat_Panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            DrawContext?.ChangeResolution();
            DrawProcess?.ChangeResolution();
        }

        private void listBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            Current.Material = (sender as ListBox).SelectedItem as Material;
        }

        private void buttonResetCamera_Click(object sender, EventArgs e)
        {
            DrawProcess.ResetCamera();
        }

        private void FormEditor_KeyDown(object sender, KeyEventArgs e)
        {
            Current.IsPress[Keys.ShiftKey] = e.Shift;
            Current.IsPress[Keys.ControlKey] = e.Control;
        }

        private void FormEditor_KeyUp(object sender, KeyEventArgs e)
        {
            Current.IsPress[Keys.ShiftKey] = e.Shift;
            Current.IsPress[Keys.ControlKey] = e.Control;
        }

        private void splitUVMat_Panel1_MouseEnter(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void splitUVMat_Panel1_MouseLeave(object sender, EventArgs e)
        {
            IsActive = false;
        }

        private void timerEvery_Tick(object sender, EventArgs e)
        {
            var mousePos = DrawTargetControl.PointToClient(Cursor.Position);
            MousePos = new SlimDX.Vector2(mousePos.X, mousePos.Y);
            toolStripStatusLabelFPS.Text = $"{Current.FPS:###.##}fps";
        }

        private void 描画リミッター解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawProcess.LimitRefresh = !(sender as ToolStripMenuItem).Checked;
        }

        private void numericRadiusOfPosSq_ValueChanged(object sender, EventArgs e)
        {
            Current.RadiusOfPositionSquare = (float)(sender as NumericUpDown).Value;
        }

        private void buttonReverseV_Click(object sender, EventArgs e)
        {
        }

        private void radioButtonRectangleSelection_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                Current.Tool = Editor.ToolBox.RectangleSelection(DrawProcess);
        }

        private void 元に戻すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Undo();
        }

        private void やり直しToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Redo();
        }
    }
}
