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
    public partial class FormSaveSelection : Form
    {
        List<SavedSelection> SavedSelections { get; }

        public FormSaveSelection()
        {
            SavedSelections = new List<SavedSelection>();
            InitializeComponent();
        }
    }

    class SavedSelection
    {
        public string Name { get; set; }
        public Command.CommandSelectVertices Command { get; }
    }
}
