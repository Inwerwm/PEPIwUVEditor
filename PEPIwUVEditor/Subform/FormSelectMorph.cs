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

namespace IwUVEditor.Subform
{
    public partial class FormSelectMorph : Form
    {
        public IPXMorph SelectedMorph => listBoxMorphs.SelectedItem as IPXMorph;

        public FormSelectMorph(IEnumerable<IPXMorph> morphs)
        {
            InitializeComponent();

            listBoxMorphs.Items.AddRange(morphs.ToArray());
            DialogResult = DialogResult.Cancel;
        }

        private void listBoxMorphs_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = SelectedMorph != null;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
