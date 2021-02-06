using DxManager;
using DxManager.Camera;
using PEPlugin;
using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor
{
    public partial class FormEditor : Form
    {
        bool initialized = false;

        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        IEnumerable<Material> Materials { get; set; }

        DxContext DxContext { get; }
        UVViewDrawProcess DrawProcess { get; set; }

        public FormEditor(IPERunArgs args)
        {
            InitializeComponent();

            Args = args;
            DxContext = DxContext.GetInstance(splitUVMat.Panel1);
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

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();
            StartProgress(Pmx.Material.Count, "モデルの読込中");

            // 材質を読込
            // 時間がかかるので進捗表示をする
            Materials = Pmx.Material.Select((material, i) =>
            {
                SetProgress(i, $"材質の読込中 : {material.Name}");
                return new Material(material, Pmx);
            });
            FinishProgress();

            // 材質表示リストボックスを構築
            SetProgress("材質の読み込み中");
            listBoxMaterial.Items.Clear();
            listBoxMaterial.Items.AddRange(Materials.ToArray());

            // 描画プロセスオブジェクトを生成
            SetProgress("描画プロセスを構成");
            DrawProcess?.Dispose();
            DrawProcess = new UVViewDrawProcess()
            {
                Camera = new DxCameraOrthographic()
                {
                    ViewVolumeSize = (4, 4),
                    ViewVolumeDepth = (0, 1)
                }
            };
            EndProgress();
        }

        private void ReDraw()
        {
            DxContext.StopDrawLoop();
            LoadModel();
            DxContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
        }

        public void DrawStart()
        {
            if (!Visible)
            {
                LoadModel();
                DxContext.StartDrawLoop(DrawProcess);
            }
        }

        #region ProgressBar

        private void StartProgress(int max, string stateText)
        {
            toolStripStatusLabelState.Text = stateText;
            toolStripProgressBarState.Visible = true;
            toolStripProgressBarState.Minimum = 0;
            toolStripProgressBarState.Maximum = max;
            toolStripProgressBarState.Value = 0;
            Refresh();
        }

        private void SetProgress(int value, string stateText)
        {
            toolStripStatusLabelState.Text = stateText;
            toolStripProgressBarState.Value = value;
            Refresh();
        }
        private void SetProgress(string stateText)
        {
            toolStripStatusLabelState.Text = stateText;
            Refresh();
        }
        private void SetProgress(int value)
        {
            toolStripProgressBarState.Value = value;
            Refresh();
        }

        private void FinishProgress()
        {
            SetProgress(toolStripProgressBarState.Maximum);
            Refresh();
        }

        private void EndProgress()
        {
            toolStripProgressBarState.Visible = false;
            toolStripStatusLabelState.Text = "準備完了";
            FinishProgress();
        }

        #endregion

        private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
            DxContext.StopDrawLoop();
        }

        private void splitUVMat_Panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            DxContext?.ChangeResolution();
        }

        private void listBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawProcess.Material = (sender as ListBox).SelectedItem as Material;
        }
    }
}
