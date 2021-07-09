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
    public partial class FormUVMorphCreationConfig : Form
    {
        public string MorphName { get; private set; }
        public int Panel { get; private set; }

        public FormUVMorphCreationConfig()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            MorphName = textBoxMorphName.Text;
            this.Panel = radioButtonBrow.Checked ? 1
                       : radioButtonEye.Checked ? 2
                       : radioButtonLip.Checked ? 3
                       : radioButtonOther.Checked ? 4
                       : throw new InvalidOperationException("UVモーフ作成設定のパネル番号に選択されるはずのない値が選択されました。");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
