using DxManager;
using DxManager.Camera;
using IwUVEditor.DirectX;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IwUVEditor
{
    public partial class FormEditor : Form
    {
        bool initialized = false;

        Editor Editor { get; }

        DxContext DxContext { get; }
        UVViewDrawProcess DrawProcess { get; set; }

        public FormEditor(IPERunArgs args)
        {
            InitializeComponent();

            Editor = new Editor(args);
            DxContext = DxContext.GetInstance(splitUVMat.Panel1);
            DxContext.RefreshRate = 120;
        }

        public void Initialize()
        {
            if (initialized)
                ReDraw();
            else
            {
                LoadModel();
                DxContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
                initialized = true;
            }
        }

        private void ReDraw()
        {
            DxContext.StopDrawLoop();
            LoadModel();
            DxContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
        }

        public void LoadModel()
        {
            Editor.LoadModel();

            // 材質表示リストボックスを構築
            listBoxMaterial.Items.Clear();
            listBoxMaterial.Items.AddRange(Editor.Materials.ToArray());

            // 描画プロセスオブジェクトを生成
            DrawProcess?.Dispose();
            DrawProcess = new UVViewDrawProcess(Editor)
            {
                Camera = new DxCameraOrthographic()
                {
                    Position = new SlimDX.Vector3(0.5f, 0.5f, -1),
                    Target = new SlimDX.Vector3(0.5f, 0.5f, 0),
                    Up = new SlimDX.Vector3(0, -1, 0),
                    ViewVolumeSize = (DxContext.TargetControl.ClientSize.Width, DxContext.TargetControl.ClientSize.Height),
                    ViewVolumeDepth = (0, 1)
                },
                RadiusOfPositionSquare = (float)numericRadiusOfPosSq.Value,
                ColorInDefault = new SlimDX.Color4(1, 0, 0, 0),
                ColorInSelected = new SlimDX.Color4(1, 1, 0, 0)
            };
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
                (DrawProcess.Camera as DxCameraOrthographic).ViewVolumeSize = (DxContext.TargetControl.Width, DxContext.TargetControl.Height);
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

            var mousePos = PointToClient(Cursor.Position);
            DrawProcess.CurrentMousePos = new SlimDX.Vector2(mousePos.X, mousePos.Y);
            toolStripStatusLabelState.Text = DrawProcess.CurrentMousePos.ToString();
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
    }
}
