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
    #region MetroRichTextBoxActionListAction

    internal class MetroRichTextBoxActionList : DesignerActionList
    {
        private readonly MetroRichTextBox _metroRichTextBox;

        public MetroRichTextBoxActionList(IComponent component) : base(component)
        {
            _metroRichTextBox = (MetroRichTextBox)component;
        }

        public Style Style
        {
            get => _metroRichTextBox.Style;
            set => _metroRichTextBox.Style = value;
        }

        public string ThemeAuthor => _metroRichTextBox.ThemeAuthor;

        public string ThemeName => _metroRichTextBox.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroRichTextBox.StyleManager;
            set => _metroRichTextBox.StyleManager = value;
        }

        public string Text
        {
            get => _metroRichTextBox.Text;
            set => _metroRichTextBox.Text = value;
        }

        public Font Font
        {
            get => _metroRichTextBox.Font;
            set => _metroRichTextBox.Font = value;
        }

        public bool ReadOnly
        {
            get => _metroRichTextBox.ReadOnly;
            set => _metroRichTextBox.ReadOnly = value;
        }

        public ContextMenuStrip ContextMenuStrip
        {
            get => _metroRichTextBox.ContextMenuStrip;
            set => _metroRichTextBox.ContextMenuStrip = value;
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
                new DesignerActionPropertyItem("ReadOnly", "ReadOnly", "Appearance", "Gets or sets a value indicating whether text in the rich text box is read-only."),
                new DesignerActionPropertyItem("ContextMenuStrip", "ContextMenuStrip", "Appearance", "Gets or sets the ContextMenuStrip associated with this control."),
            };

            return items;
        }
    }

    #endregion
}