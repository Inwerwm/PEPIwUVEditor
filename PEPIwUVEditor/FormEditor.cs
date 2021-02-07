using DxManager;
using DxManager.Camera;
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
        private float cameraScale;

        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }
        IEnumerable<Material> Materials { get; set; }

        DxContext DxContext { get; }
        UVViewDrawProcess DrawProcess { get; set; }

        /// <summary>
        /// 表示スケール
        /// </summary>
        float CameraScale
        {
            get => cameraScale;
            set
            {
                cameraScale = value < -1 ? -1 : value > 1 ? 1 : value;
                ApplyCameraScale();
            }
        }
        /// <summary>
        /// 最大縮小倍率(大きいほど縮小できる)
        /// </summary>
        float CameraAmplitude { get; set; }
        /// <summary>
        /// ホイール1回転あたりの拡縮量
        /// </summary>
        float CameraStep { get; set; }

        public FormEditor(IPERunArgs args)
        {
            InitializeComponent();

            Args = args;
            DxContext = DxContext.GetInstance(splitUVMat.Panel1);

            CameraAmplitude = 10;
            CameraStep = 0.05f;
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
                    ViewVolumeDepth = (0, 1)
                }
            };
            CameraScale = 0.5f;
            EndProgress();
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

        /// <summary>
        /// シグモイド関数
        /// </summary>
        /// <param name="x">推奨[0, 1]区間]</param>
        /// <returns>(0, 1)区間</returns>
        private double Sigmoid(double x) => 1 / (1 + Math.Pow(Math.E, -7.5 * x));
        private void ApplyCameraScale()
        {
            if (DrawProcess is null)
                return;
            var cameraRange = (float)(Sigmoid(CameraScale) * CameraAmplitude);
            (DrawProcess.Camera as DxCameraOrthographic).ViewVolumeSize = (cameraRange, cameraRange);
            toolStripStatusLabelState.Text = $"CameraScale:{CameraScale}, CameraRange:{cameraRange}";
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
        }

        private void splitUVMat_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            CameraScale += -1 * e.Delta * (CameraStep / 120);
        }

        private void listBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawProcess.CurrentMaterial = (sender as ListBox).SelectedItem as Material;
        }
    }
}
