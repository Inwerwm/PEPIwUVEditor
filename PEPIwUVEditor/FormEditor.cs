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
        IPERunArgs Args { get; }
        IPXPmx Pmx { get; set; }

        public DxContext DxContext { get; }
        public UVViewDrawProcess DrawProcess { get; }

        public FormEditor(IPERunArgs args)
        {
            InitializeComponent();

            Args = args;

            DxContext = DxContext.GetInstance(splitUVMat.Panel1);
            DrawProcess = new UVViewDrawProcess(Args.Host.Connector.Pmx.GetCurrentState())
            {
                Camera = new DxCameraOrthographic()
                {
                    ViewVolumeSize = (2, 2),
                    ViewVolumeDepth = (0, 1)
                }
            };

            DxContext.AddDrawloop(DrawProcess, Properties.Resources.Shader);
        }

        public void LoadModel()
        {
            Pmx = Args.Host.Connector.Pmx.GetCurrentState();

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
