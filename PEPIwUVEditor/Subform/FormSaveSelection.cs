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

        Action<Material> VertexUpdater { get; }
        EditorStates Current { get; }
        List<SavedSelection> SavedSelections { get; }

        internal FormSaveSelection(EditorStates editorStates, Action<Material> vertexUpdater)
        {
            Current = editorStates;
            VertexUpdater = vertexUpdater;

            InitializeComponent();

            SavedSelections = new List<SavedSelection>();
            dataGridViewSelections.DataSource = SavedSelections;
        }

        private Dictionary<TKey, TValue> CopyDictionary<TKey, TValue>(Dictionary<TKey, TValue> source) =>
            source.ToDictionary(p => p.Key, p => p.Value);

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SavedSelections.Add(new SavedSelection(
                $"{SaveCount} : {Current.Material.Name} - {Current.Material.IsSelected.Count(p => p.Value)}",
                new CommandSelectVertices(Current.Material, CopyDictionary(Current.Material.IsSelected),() => VertexUpdater(Current.Material))
            ));
            SaveCount++;
        }
    }

    class SavedSelection
    {
        public string Name { get; set; }
        public CommandSelectVertices Command { get; }

        public SavedSelection(string name, CommandSelectVertices command)
        {
            Name = name;
            Command = command;
        }
    }
}
