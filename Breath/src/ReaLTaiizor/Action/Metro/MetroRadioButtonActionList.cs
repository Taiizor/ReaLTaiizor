#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroRadioButtonActionListAction

    internal class MetroRadioButtonActionList : DesignerActionList
    {
        private readonly MetroRadioButton _metroRadioButton;

        public MetroRadioButtonActionList(IComponent component) : base(component)
        {
            _metroRadioButton = (MetroRadioButton)component;
        }

        public Style Style
        {
            get => _metroRadioButton.Style;
            set => _metroRadioButton.Style = value;
        }

        public string ThemeAuthor => _metroRadioButton.ThemeAuthor;

        public string ThemeName => _metroRadioButton.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroRadioButton.StyleManager;
            set => _metroRadioButton.StyleManager = value;
        }

        public string Text
        {
            get => _metroRadioButton.Text;
            set => _metroRadioButton.Text = value;
        }

        public bool Checked
        {
            get => _metroRadioButton.Checked;
            set => _metroRadioButton.Checked = value;
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

            };
            return items;
        }
    }

    #endregion
}