#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroTextBoxActionListAction

    internal class MetroTextBoxActionList : DesignerActionList
    {
        private readonly MetroTextBox _metroTextBox;

        public MetroTextBoxActionList(IComponent component) : base(component)
        {
            _metroTextBox = (MetroTextBox)component;
        }

        public Style Style
        {
            get => _metroTextBox.Style;
            set => _metroTextBox.Style = value;
        }

        public string ThemeAuthor => _metroTextBox.ThemeAuthor;

        public string ThemeName => _metroTextBox.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroTextBox.StyleManager;
            set => _metroTextBox.StyleManager = value;
        }

        public string Text
        {
            get => _metroTextBox.Text;
            set => _metroTextBox.Text = value;
        }

        public Font Font
        {
            get => _metroTextBox.Font;
            set => _metroTextBox.Font = value;
        }

        public bool ReadOnly
        {
            get => _metroTextBox.ReadOnly;
            set => _metroTextBox.ReadOnly = value;
        }

        public bool UseSystemPasswordChar
        {
            get => _metroTextBox.UseSystemPasswordChar;
            set => _metroTextBox.UseSystemPasswordChar = value;
        }

        public bool Multiline
        {
            get => _metroTextBox.Multiline;
            set => _metroTextBox.Multiline = value;
        }

        public string WatermarkText
        {
            get => _metroTextBox.WatermarkText;
            set => _metroTextBox.WatermarkText = value;
        }

        public ContextMenuStrip ContextMenuStrip
        {
            get => _metroTextBox.ContextMenuStrip;
            set => _metroTextBox.ContextMenuStrip = value;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new()
            {
                new DesignerActionHeaderItem("Metro"),
                new DesignerActionPropertyItem("StyleManager", "StyleManager", "Metro", "Gets or sets the stylemanager for the control."),
                new DesignerActionPropertyItem("Style", "Style", "Metro", "Gets or sets the style."),

                new DesignerActionHeaderItem("Informations"),
                new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
                new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

                new DesignerActionHeaderItem("Appearance"),
                new DesignerActionPropertyItem("Text", "Text", "Appearance", "Gets or sets the The text associated with the control."),
                new DesignerActionPropertyItem("Font", "Font", "Appearance", "Gets or sets the The font associated with the control."),
                new DesignerActionPropertyItem("ReadOnly", "ReadOnly", "Appearance", "Gets or sets a value indicating whether text in the text box is read-only."),
                new DesignerActionPropertyItem("UseSystemPasswordChar", "UseSystemPasswordChar", "Appearance", "Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character."),
                new DesignerActionPropertyItem("Multiline", "Multiline", "Appearance", "Gets or sets a value indicating whether this is a multiline TextBox control."),
                new DesignerActionPropertyItem("WatermarkText", "WatermarkText", "Appearance", "Gets or sets the text in the TextBox while being empty."),
                new DesignerActionPropertyItem("ContextMenuStrip", "ContextMenuStrip", "Appearance", "Gets or sets the ContextMenuStrip associated with this control."),
            };
            return items;
        }
    }

    #endregion
}