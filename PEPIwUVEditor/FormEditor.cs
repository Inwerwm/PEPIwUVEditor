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
            Refresh();
            LoadModel();
            DxContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
            toolStripStatusLabelState.Text = "準備完了";
        }

        public void LoadModel()
        {
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();
            Materials = Pmx.Material.Select(material => new Material(material));
            listBoxMaterial.Items.AddRange(Materials.ToArray());
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
