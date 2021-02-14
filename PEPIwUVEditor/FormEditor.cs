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
        ViewControl ViewControl { get; }
        InputManager Current { get; }

        public FormEditor(ViewControl viewControl, InputManager inputManager)
        {
            ViewControl = viewControl;
            Current = inputManager;

            InitializeComponent();
            InitializeCurrent();

            timerEvery.Enabled = true;
        }

        private void InitializeCurrent()
        {
            Current.RadiusOfPositionSquare = (float)numericRadiusOfPosSq.Value;
        }

        internal Control DrawTargetControl => splitUVMat.Panel1;

        internal void LoadMaterials(Material[] materials)
        {
            listBoxMaterial.Items.Clear();
            listBoxMaterial.Items.AddRange(materials);
        }

        private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ViewControl.StopDraw();
        }

        private void splitUVMat_Panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            ViewControl.ChangeScreenSize();
        }

        private void splitUVMat_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        private void listBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            Current.Material = (sender as ListBox).SelectedItem as Material;
        }

        private void buttonResetCamera_Click(object sender, EventArgs e)
        {
            ViewControl.ResetCamera();
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
            Current.IsActive = true;
        }

        private void splitUVMat_Panel1_MouseLeave(object sender, EventArgs e)
        {
            Current.IsActive = false;
        }

        private void timerEvery_Tick(object sender, EventArgs e)
        {
            var mousePos = DrawTargetControl.PointToClient(Cursor.Position);
            Current.MousePos = new SlimDX.Vector2(mousePos.X, mousePos.Y);
            toolStripStatusLabelFPS.Text = $"{Current.FPS:###.##}fps";
        }

        private void 描画リミッター解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewControl.ChangeRefreshLimitTo(!(sender as ToolStripMenuItem).Checked);
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
                Current.Tool = Tool.RectangleSelection;
        }

        private void splitUVMat_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void 元に戻すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewControl.OrderUndo();
        }

        private void やり直しToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewControl.OrderRedo();
        }
    }
}
