using ColorSelector;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IwUVEditor.Subform
{
    public partial class FormColorSettings : Form
    {
        private bool isActive;

        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                if (isActive)
                    SyncColor();
                else if (CurrentSetting != null)
                    colorSelector1.ColorChanged -= CurrentSetting.ColorSetter;
            }
        }

        public Color SelectionRectangleColor
        {
            get => panelSelectionRect.BackColor;
            set
            {
                panelSelectionRect.BackColor = value;
                if (IsActive)
                    SelectionRectangleColorChanged(value);
            }
        }

        public Color VertexMeshColor
        {
            get => panelVtxMesh.BackColor;
            set
            {
                panelVtxMesh.BackColor = value;
                if (IsActive)
                    VertexMeshColorChanged(value);
            }
        }

        public Color SelectedVertexColor
        {
            get => panelSelectedVtx.BackColor;
            set {
                panelSelectedVtx.BackColor = value;
                if (IsActive)
                    SelectedVertexColorChanged(value);
            }
        }

        public Color BackgroundColor
        {
            get => panelBackground.BackColor;
            set {
                panelBackground.BackColor = value;
                if (IsActive)
                    BackgroundColorChanged(value);
            }
        }

        public event ColorHandler SelectionRectangleColorChanged = delegate { };
        public event ColorHandler VertexMeshColorChanged = delegate { };
        public event ColorHandler SelectedVertexColorChanged = delegate { };
        public event ColorHandler BackgroundColorChanged = delegate { };

        private List<ColorSettingControler> Settings { get; }
        ColorSettingControler CurrentSetting { get; set; }

        public FormColorSettings()
        {
            IsActive = false;
            InitializeComponent();

            Settings = new List<ColorSettingControler>
            {
                (radioButtonSelectionRect, panelSelectionRect, (c) => SelectionRectangleColor = c),
                (radioButtonVtxMesh, panelVtxMesh, (c) => VertexMeshColor = c),
                (radioButtonSelectedVtx, panelSelectedVtx, (c) => SelectedVertexColor = c),
                (radioButtonBackground, panelBackground, (c) => BackgroundColor = c),
            };
        }

        private void SyncColor()
        {
            if (CurrentSetting != null)
                colorSelector1.ColorChanged -= CurrentSetting.ColorSetter;
            CurrentSetting = Settings.First(s => s.Selector.Checked);
            colorSelector1.Color = CurrentSetting.Panel.BackColor;
            colorSelector1.ColorChanged += CurrentSetting.ColorSetter;
        }

        private void FormColorSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            IsActive = false;
            Visible = false;
        }

        private void RadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            if (IsActive && (sender as RadioButton).Checked)
                SyncColor();
        }
    }

    internal class ColorSettingControler
    {
        public RadioButton Selector;
        public Panel Panel;
        public ColorHandler ColorSetter;

        public ColorSettingControler(RadioButton selector, Panel panel, ColorHandler colorSetter)
        {
            Selector = selector;
            Panel = panel;
            ColorSetter = colorSetter;
        }

        public override bool Equals(object obj)
        {
            return obj is ColorSettingControler other &&
                   EqualityComparer<RadioButton>.Default.Equals(Selector, other.Selector) &&
                   EqualityComparer<Panel>.Default.Equals(Panel, other.Panel) &&
                   EqualityComparer<ColorHandler>.Default.Equals(ColorSetter, other.ColorSetter);
        }

        public override int GetHashCode()
        {
            int hashCode = 2095399324;
            hashCode = hashCode * -1521134295 + EqualityComparer<RadioButton>.Default.GetHashCode(Selector);
            hashCode = hashCode * -1521134295 + EqualityComparer<Panel>.Default.GetHashCode(Panel);
            hashCode = hashCode * -1521134295 + EqualityComparer<ColorHandler>.Default.GetHashCode(ColorSetter);
            return hashCode;
        }

        public void Deconstruct(out RadioButton selector, out Panel panel, out ColorHandler colorSetter)
        {
            selector = Selector;
            panel = Panel;
            colorSetter = ColorSetter;
        }

        public static implicit operator (RadioButton Selector, Panel Panel, ColorHandler ColorSetter)(ColorSettingControler value)
        {
            return (value.Selector, value.Panel, value.ColorSetter);
        }

        public static implicit operator ColorSettingControler((RadioButton Selector, Panel Panel, ColorHandler ColorSetter) value)
        {
            return new ColorSettingControler(value.Selector, value.Panel, value.ColorSetter);
        }
    }
}
