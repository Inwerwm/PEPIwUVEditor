using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IwUVEditor.Log
{
    public partial class FormDebugLog : Form
    {
        public FormDebugLog()
        {
            InitializeComponent();
        }

        public void Write(string log)
        {
            if (checkBoxEnable.Checked)
                textBoxLog.Text = log;
        }

        public void Append(string log)
        {
            if (checkBoxEnable.Checked)
                textBoxLog.AppendText(Environment.NewLine + log);
        }

        public void Clear() => textBoxLog.Clear();

        private void FormDebugLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
