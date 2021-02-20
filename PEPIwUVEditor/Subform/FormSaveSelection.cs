using IwUVEditor.Command;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.StateContainer;
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
        private int SaveCount { get; set; }

        internal Action VertexUpdater { get; set; }
        internal Action<Material, IEditorCommand> CommandInvoker { get; set; }
        EditorStates Current { get; }

        internal FormSaveSelection(EditorStates editorStates)
        {
            Current = editorStates;

            InitializeComponent();

        }

        private Dictionary<TKey, TValue> CopyDictionary<TKey, TValue>(Dictionary<TKey, TValue> source) =>
            source.ToDictionary(p => p.Key, p => p.Value);

        private void buttonSave_Click(object sender, EventArgs e)
        {
            dataGridViewSelections.Rows.Add(
                SaveCount.ToString("00"),
                Current.Material.IsSelected.Count(p => p.Value),
                Current.Material,
                new CommandSelectVertices(Current.Material, CopyDictionary(Current.Material.IsSelected), VertexUpdater)
            );
            SaveCount++;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if(dataGridViewSelections.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewSelections.SelectedRows[0];
                CommandSelectVertices cmd = selectedRow.Cells["SelectionCommand"].Value as CommandSelectVertices;
                cmd.Mode = (ModifierKeys & Keys.Shift) == Keys.Shift ? Command.SelectionMode.Union :
                           (ModifierKeys & Keys.Control) == Keys.Control ? Command.SelectionMode.Difference :
                           Command.SelectionMode.Create;

                CommandInvoker(
                    selectedRow.Cells["SelectionMaterial"].Value as Material,
                    cmd
                );
            }
        }

        private void toolStripStatusLabelForefront_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            (sender as ToolStripStatusLabel).BackColor = TopMost ? SystemColors.ActiveCaption : SystemColors.ButtonFace;
        }

        private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSelections.SelectedRows.Count > 0)
                dataGridViewSelections.Rows.Remove(dataGridViewSelections.SelectedRows[0]);
        }

        private void dataGridViewSelections_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                dataGridViewSelections.Rows[e.RowIndex].Cells[0].Selected = true;
        }
    }
}
