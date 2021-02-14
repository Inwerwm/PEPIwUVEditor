using DxManager;
using DxManager.Camera;
using IwUVEditor.DirectX;
using IwUVEditor.Manager;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IwUVEditor
{
    internal partial class FormEditor : Form
    {
        public InputManager Current { get; }

        public FormEditor(InputManager inputManager)
        {
            InitializeComponent();
            Current = inputManager;
        }

        internal Control DrawTargetControl => splitUVMat.Panel1;
        internal float RadiusOfPositionSquare => (float)numericRadiusOfPosSq.Value;
        internal void LoadMaterials(Material[] materials)
        {
            listBoxMaterial.Items.Clear();
            listBoxMaterial.Items.AddRange(materials);
        }

        private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
            DxContext.StopDrawLoop();
        }

        private void splitUVMat_Panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            DxContext?.ChangeResolution();
            if (DrawProcess != null)
            {
                DrawProcess.ChangeResolution();
            }
        }

        private void splitUVMat_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        private void listBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawProcess.CurrentMaterial = (sender as ListBox).SelectedItem as Material;
        }

        private void buttonResetCamera_Click(object sender, EventArgs e)
        {
            DrawProcess.ResetCamera();
        }

        private void FormEditor_KeyDown(object sender, KeyEventArgs e)
        {
            DrawProcess.IsPress[Keys.ShiftKey] = e.Shift;
            DrawProcess.IsPress[Keys.ControlKey] = e.Control;
        }

        private void FormEditor_KeyUp(object sender, KeyEventArgs e)
        {
            DrawProcess.IsPress[Keys.ShiftKey] = e.Shift;
            DrawProcess.IsPress[Keys.ControlKey] = e.Control;
        }

        private void splitUVMat_Panel1_MouseEnter(object sender, EventArgs e)
        {
            if (DrawProcess is null)
                return;
            DrawProcess.IsActive = true;
        }

        private void splitUVMat_Panel1_MouseLeave(object sender, EventArgs e)
        {
            if (DrawProcess is null)
                return;
            DrawProcess.IsActive = false;
        }

        private void timerEvery_Tick(object sender, EventArgs e)
        {
            if (DrawProcess is null)
                return;

            var mousePos = DxContext.TargetControl.PointToClient(Cursor.Position);
            DrawProcess.CurrentMousePos = new SlimDX.Vector2(mousePos.X, mousePos.Y);
            toolStripStatusLabelState.Text = $"{mousePos} => {DrawProcess.CurrentMousePos}, Drag State : {DrawProcess.LeftDrag.Start} - {DrawProcess.LeftDrag.Current} - {DrawProcess.LeftDrag.End}";
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
                Editor.CurrentTool = Tool.RectangleSelection;
        }

        private void splitUVMat_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
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
