using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor.Subform
{
    public partial class FormColorSettings : Form
    {
        internal Color SelectionRectangleColor
        {
            get => panelSelectionRect.BackColor;
            set => panelSelectionRect.BackColor = value;
        }

        internal Color VertexMeshColor
        {
            get => panelVtxMesh.BackColor;
            set => panelVtxMesh.BackColor = value;
        }

        internal Color SelectedVertexColor
        {
            get => panelSelectedVtx.BackColor;
            set => panelSelectedVtx.BackColor = value;
        }

        internal Color BackgroundColor
        {
            get => panelBackground.BackColor;
            set => panelBackground.BackColor = value;
        }

        public FormColorSettings()
        {
            InitializeComponent();
        }

        private void FormColorSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}
