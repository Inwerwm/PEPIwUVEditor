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

        internal event CatchExceptionEventHandler CatchException;

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

        FormSaveSelection SelectionSaver { get; set; }
        FormColorSettings ColorSettings { get; }
        bool IsListeningColorSettingEvents { get; set; }

        internal Control DrawTargetControl => splitUVMat.Panel1;

        public FormEditor(Editor editor, EditorStates inputManager)
        {
            Editor = editor;
            Current = inputManager;
            Input = new InputStates();

            InitializeComponent();

            SelectionSaver = new FormSaveSelection(Current) { CommandInvoker = Editor.Do };
            ColorSettings = new FormColorSettings();

            Editor.EditParameters.RotationCenterChanged += (value) =>
            {
                numericRotCenterX.Value = (decimal)value.X;
                numericRotCenterY.Value = (decimal)value.Y;
            };
            Editor.EditParameters.ScaleCenterChanged += (value) =>
            {
                numericScaleCenterX.Value = (decimal)value.X;
                numericScaleCenterY.Value = (decimal)value.Y;
            };
        }

        internal void InitializeWhenStartDrawing()
        {
            DrawProcess.RadiusOfPositionSquare = (float)numericRadiusOfPosSq.Value;

            Editor.UpdateDraw = () => {
                DrawProcess.UpdateDrawingVertices();
                DisplayEditState();
            };
            SelectionSaver.VertexUpdater = DrawProcess.UpdateDrawingVertices;

            Current.Tool = Editor.ToolBox.RectangleSelection(DrawProcess);

            // 色設定フォームに色を反映
            Tool.IEditTool recSel;
            if (Editor.ToolBox.InstanceOf.TryGetValue(typeof(Tool.RectangleSelection), out recSel))
                ColorSettings.SelectionRectangleColor = (recSel as Tool.RectangleSelection).RectangleColor.ToColor();
            ColorSettings.VertexMeshColor = DrawProcess.ColorInDefault.ToColor();
            ColorSettings.SelectedVertexColor = DrawProcess.ColorInSelected.ToColor();
            ColorSettings.BackgroundColor = DrawProcess.BackgroundColor.ToColor();

            DisplayEditState();
            timerEvery.Enabled = true;
        }

        /// <summary>
        /// 編集された状態ならタイトルにそれを示す
        /// </summary>
        void DisplayEditState()
        {
            Text = Editor.IsEdited ? "UV編集 *" : "UV編集";
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
            try
            {
                if (!Input.IsActive)
                    return;

                Input.ReadMouseInput(e, DrawProcess.ScreenPosToWorldPos);

                float modifier = (Input.IsPress[Keys.ShiftKey] ? 4f : 1f) / (Input.IsPress[Keys.ControlKey] ? 4f : 1f);

                if(Input.Wheel.IsScrolling)
                    DrawProcess.Scale.WheelDelta += Input.Wheel.Delta * modifier;
                if (Input.IsClicking[MouseButtons.Middle])
                    DrawProcess.ShiftOffset += modifier * new Vector3(1f * e.X / DrawTargetControl.Width, -1f * e.Y / DrawTargetControl.Height, 0) / DrawProcess.Scale.Scale;

                Editor.DriveTool(Input);
            }
            catch (Exception ex)
            {
                CatchException?.Invoke(ex);
            }
        }

        private void splitUVMat_Panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            DrawContext?.ChangeResolution();
            DrawProcess?.ChangeResolution();
        }

        private void listBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            Current.Material = (sender as ListBox).SelectedItem as Material ?? Current.Material;
        }

        private void buttonResetCamera_Click(object sender, EventArgs e)
        {
            DrawProcess.ResetCamera();
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

            Input.IsPress[Keys.ShiftKey] = (ModifierKeys & Keys.Shift) == Keys.Shift;
            Input.IsPress[Keys.ControlKey] = (ModifierKeys & Keys.Control) == Keys.Control;

            toolStripStatusLabelFPS.Text = $"{DrawProcess.CurrentFPS:###.##}fps";
            toolStripStatusLabelState.Text = $"Shift:{Input.IsPress[Keys.ShiftKey]}, Ctrl:{Input.IsPress[Keys.ControlKey]}";
        }

        private void 描画リミッター解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawProcess.LimitRefresh = !(sender as ToolStripMenuItem).Checked;
        }

        private void numericRadiusOfPosSq_ValueChanged(object sender, EventArgs e)
        {
            DrawProcess.RadiusOfPositionSquare = (float)(sender as NumericUpDown).Value;
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
            if (!IsListeningColorSettingEvents)
            {
                ColorSettings.SelectionRectangleColorChanged +=
                    new ColorSelector.ColorHandler(c =>
                    {
                        Tool.IEditTool rs;
                        if (Editor.ToolBox.InstanceOf.TryGetValue(typeof(Tool.RectangleSelection), out rs))
                            (rs as Tool.RectangleSelection).RectangleColor = new Color4(c);
                    });

                ColorSettings.VertexMeshColorChanged +=
                    new ColorSelector.ColorHandler(c => DrawProcess.ColorInDefault = new Color4(c));
                ColorSettings.SelectedVertexColorChanged +=
                    new ColorSelector.ColorHandler(c => DrawProcess.ColorInSelected = new Color4(c));
                ColorSettings.BackgroundColorChanged +=
                    new ColorSelector.ColorHandler(c => DrawProcess.BackgroundColor = new Color4(c));

                IsListeningColorSettingEvents = true;
            }

            ColorSettings.IsActive = true;
            ColorSettings.Visible = true;
        }

        private void 頂点選択保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionSaver.Visible = true;
        }

        private void radioButtonRectangleSelection_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                Current.Tool = Editor.ToolBox.RectangleSelection(DrawProcess);
        }

        private void radioButtonMove_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                Current.Tool = Editor.ToolBox.MoveVertices(DrawProcess);
        }

        private void radioButtonRotate_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                Current.Tool = Editor.ToolBox.RotateVertices(DrawProcess);
        }

        private void radioButtonScale_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                Current.Tool = Editor.ToolBox.ScaleVertices(DrawProcess);
        }

        private void 座標のコピーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.CopyPosition();
        }

        private void 座標のペーストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.PastePosition();
        }

        private void デバッグログToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.DebugLog.Show();
        }

        private void 再読込ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Editor.Reset();
        }

        private void UVを反映ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.SendModel();
        }

        private void buttonReverseV_Click(object sender, EventArgs e)
        {
            Editor.ReverseVerticesVertical();
        }

        private void buttonReverseH_Click(object sender, EventArgs e)
        {
            Editor.ReverseVerticesHorizontal();
        }

        private void numericRotCenterX_ValueChanged(object sender, EventArgs e)
        {
            Editor.EditParameters.RotationCenter = new Vector3((float)numericRotCenterX.Value, Editor.EditParameters.RotationCenter.Y, 0);
        }

        private void numericRotCenterY_ValueChanged(object sender, EventArgs e)
        {
            Editor.EditParameters.RotationCenter = new Vector3(Editor.EditParameters.RotationCenter.X, (float)numericRotCenterY.Value, 0);
        }

        private void numericScaleCenterX_ValueChanged(object sender, EventArgs e)
        {
            Editor.EditParameters.ScaleCenter = new Vector3((float)numericScaleCenterX.Value, Editor.EditParameters.ScaleCenter.Y, 0);
        }

        private void numericScaleCenterY_ValueChanged(object sender, EventArgs e)
        {
            Editor.EditParameters.ScaleCenter = new Vector3(Editor.EditParameters.ScaleCenter.X, (float)numericScaleCenterY.Value, 0);
        }

        private void buttonApplyNumericEdit_Click(object sender, EventArgs e)
        {
            Editor.EditParameters.MoveOffset = new Vector3((float)numericMoveX.Value, (float)numericMoveY.Value, 0);
            Editor.EditParameters.RotationAngle = -(float)((double)(numericRotAngle.Value / 180m) * Math.PI);
            Editor.EditParameters.ScaleRatio = new Vector3((float)numericScaleRatioX.Value, (float)numericScaleRatioY.Value, 1);

            Editor.ApplyEditWithValue();
        }

        private void buttonSelectContinuousFaces_Click(object sender, EventArgs e)
        {

        }
    }
}
