#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroCheckBoxActionListAction

    internal class MetroCheckBoxActionList : DesignerActionList
    {
        private readonly MetroCheckBox _metroCheckBox;

        public MetroCheckBoxActionList(IComponent component) : base(component)
        {
            _metroCheckBox = (MetroCheckBox)component;
        }

        public Style Style
        {
            get => _metroCheckBox.Style;
            set => _metroCheckBox.Style = value;
        }

        public string ThemeAuthor => _metroCheckBox.ThemeAuthor;

        public string ThemeName => _metroCheckBox.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroCheckBox.StyleManager;
            set => _metroCheckBox.StyleManager = value;
        }

        public string Text
        {
            get => _metroCheckBox.Text;
            set => _metroCheckBox.Text = value;
        }

        public bool Checked
        {
            get => _metroCheckBox.Checked;
            set => _metroCheckBox.Checked = value;
        }

        public SignStyle SignStyle
        {
            get => _metroCheckBox.SignStyle;
            set => _metroCheckBox.SignStyle = value;
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
                new DesignerActionPropertyItem("Checked", "Checked", "Appearance", "Gets or sets a value indicating whether the control is checked."),
                new DesignerActionPropertyItem("SignStyle", "SignStyle", "Appearance", "Gets or sets the the sign style of check.")
            };
            return items;
        }
    }

    #endregion
}