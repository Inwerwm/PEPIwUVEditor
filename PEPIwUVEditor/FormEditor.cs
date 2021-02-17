using DxManager;
using IwUVEditor.DirectX;
using IwUVEditor.StateContainer;
using IwUVEditor.Subform;
using SlimDX;
using SlimDX.RawInput;
using System;
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
        InputStates Input { get; }

        FormColorSettings ColorSettings { get; }

        internal Control DrawTargetControl => splitUVMat.Panel1;

        public FormEditor(Editor editor, EditorStates inputManager)
        {
            Editor = editor;
            Current = inputManager;
            Input = new InputStates();

            InitializeComponent();

            ColorSettings = new FormColorSettings();
        }

        internal void InitializeWhenStartDrawing()
        {
            DrawProcess.RadiusOfPositionSquare = (float)numericRadiusOfPosSq.Value;
            Current.Tool = Editor.ToolBox.RectangleSelection(DrawProcess);
            timerEvery.Enabled = true;
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
            if (!Input.IsActive)
                return;

            float modifier = (Input.IsPress[Keys.ShiftKey] ? 4f : 1f) / (Input.IsPress[Keys.ControlKey] ? 4f : 1f);

            switch (e.ButtonFlags)
            {
                case MouseButtonFlags.MouseWheel:
                    DrawProcess.Scale.WheelDelta += e.WheelDelta * modifier;
                    break;
                case MouseButtonFlags.MiddleUp:
                    Input.IsClicking[MouseButtons.Middle] = false;
                    break;
                case MouseButtonFlags.MiddleDown:
                    Input.IsClicking[MouseButtons.Middle] = true;
                    break;
                case MouseButtonFlags.RightUp:
                    Input.IsClicking[MouseButtons.Right] = false;
                    break;
                case MouseButtonFlags.RightDown:
                    Input.IsClicking[MouseButtons.Right] = true;
                    break;
                case MouseButtonFlags.LeftUp:
                    Input.IsClicking[MouseButtons.Left] = false;
                    break;
                case MouseButtonFlags.LeftDown:
                    Input.IsClicking[MouseButtons.Left] = true;
                    break;
                default:
                    break;
            }

            if (Input.IsClicking[MouseButtons.Middle])
                DrawProcess.ShiftOffset += modifier * new Vector3(1f * e.X / DrawTargetControl.Width, -1f * e.Y / DrawTargetControl.Height, 0) / DrawProcess.Scale.Scale;

            Input.MouseLeft.ReadState(DrawProcess.ScreenPosToWorldPos(Input.MousePos), Input.IsClicking[MouseButtons.Left]);
            Editor.DriveTool(Input.MouseLeft, Input.IsPress);
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
            Input.IsPress[Keys.ShiftKey] = e.Shift;
            Input.IsPress[Keys.ControlKey] = e.Control;
        }

        private void FormEditor_KeyUp(object sender, KeyEventArgs e)
        {
            Input.IsPress[Keys.ShiftKey] = e.Shift;
            Input.IsPress[Keys.ControlKey] = e.Control;
        }

        private void splitUVMat_Panel1_MouseEnter(object sender, EventArgs e)
        {
            Input.IsActive = true;
        }

        private void splitUVMat_Panel1_MouseLeave(object sender, EventArgs e)
        {
            Input.IsActive = false;
        }

        private void timerEvery_Tick(object sender, EventArgs e)
        {
            var mousePos = DrawTargetControl.PointToClient(Cursor.Position);
            Input.MousePos = new Vector2(mousePos.X, mousePos.Y);
            toolStripStatusLabelFPS.Text = $"{DrawProcess.CurrentFPS:###.##}fps";
        }

        private void 描画リミッター解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawProcess.LimitRefresh = !(sender as ToolStripMenuItem).Checked;
        }

        private void numericRadiusOfPosSq_ValueChanged(object sender, EventArgs e)
        {
            DrawProcess.RadiusOfPositionSquare = (float)(sender as NumericUpDown).Value;
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

        private void 色を変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tool.IEditTool recSel;
            if (Editor.ToolBox.InstanceOf.TryGetValue(typeof(Tool.RectangleSelection), out recSel))
                ColorSettings.SelectionRectangleColor = (recSel as Tool.RectangleSelection).RectangleColor.ToColor();
            

            ColorSettings.Visible = true;
        }
    }
}
