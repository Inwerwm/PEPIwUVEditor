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
                return;

            toolStripStatusLabelState.Text = "モデルの読込中";
            toolStripProgressBarState.Visible = true;
            Refresh();
            LoadModel();
            DxContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
            toolStripStatusLabelState.Text = "準備完了";
            toolStripProgressBarState.Visible = false;
        }

        public void LoadModel()
        {
            // モデルを読込
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

            // 材質を読込
            // 時間がかかるので進捗表示をする
            toolStripProgressBarState.Maximum = Pmx.Material.Count;
            Materials = Pmx.Material.Select((material, i) =>
            {
                toolStripProgressBarState.Value = i;
                toolStripStatusLabelState.Text = $"材質の読込中 : {material.Name}";
                Refresh();
                return new Material(material);
            });
            toolStripProgressBarState.Value = toolStripProgressBarState.Maximum;

            // 材質表示リストボックスを構築
            toolStripStatusLabelState.Text = "材質の読み込み中";
            listBoxMaterial.Items.Clear();
            listBoxMaterial.Items.AddRange(Materials.ToArray());

            // 描画プロセスオブジェクトを生成
            toolStripStatusLabelState.Text = "描画プロセスを構成";
            DrawProcess?.Dispose();
            DrawProcess = new UVViewDrawProcess(Materials)
            {
                Camera = new DxCameraOrthographic()
                {
                    ViewVolumeSize = (4, 4),
                    ViewVolumeDepth = (0, 1)
                }
            };
        }

        private void ReDraw()
        {
            DxContext.StopDrawLoop();
            LoadModel();
            DxContext.StartDrawLoop(DrawProcess);
        }

        public void DrawStart()
        {
            if (!Visible)
            {
                LoadModel();
                DxContext.StartDrawLoop(DrawProcess);
            }
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
    }
}
