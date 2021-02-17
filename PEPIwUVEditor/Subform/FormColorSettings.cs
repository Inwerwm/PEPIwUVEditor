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
