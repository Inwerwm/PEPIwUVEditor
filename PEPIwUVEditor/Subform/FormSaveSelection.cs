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
            listBoxSaved.Items.Insert(0, new SavedSelection(
                $"{SaveCount:00} : {Current.Material.Name}",
                Current.Material.IsSelected.Count(p => p.Value),
                new CommandSelectVertices(Current.Material, CopyDictionary(Current.Material.IsSelected),VertexUpdater),
                Current.Material
            ));
            SaveCount++;
        }

        private void FormSaveSelection_Load(object sender, EventArgs e)
        {
            SavedSelection selectedSelection = listBoxSaved.SelectedItem as SavedSelection;
            if (!(selectedSelection is null))
                CommandInvoker(selectedSelection.Material, selectedSelection.Command);
        }
    }

    internal class SavedSelection
    {
        internal string Name { get; set; }
        internal int Count { get; }
        internal CommandSelectVertices Command { get; }
        internal Material Material { get; }

        internal SavedSelection(string name, int count, CommandSelectVertices command, Material material)
        {
            Name = name;
            Count = count;
            Command = command;
            Material = material;
        }

        public override string ToString()
        {
            return $"{Name} - {Count}";
        }
    }
}
