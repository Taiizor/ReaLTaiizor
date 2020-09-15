#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Controls;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroSwitchActionListAction

    internal class MetroSwitchActionList : DesignerActionList
    {
        private readonly MetroSwitch _metroSwitch;

        public MetroSwitchActionList(IComponent component) : base(component)
        {
            _metroSwitch = (MetroSwitch)component;
        }

        public Style Style
        {
            get => _metroSwitch.Style;
            set => _metroSwitch.Style = value;
        }

        public string ThemeAuthor => _metroSwitch.ThemeAuthor;

        public string ThemeName => _metroSwitch.ThemeName;

        public MetroStyleManager MetroStyleManager
        {
            get => _metroSwitch.MetroStyleManager;
            set => _metroSwitch.MetroStyleManager = value;
        }

        public string Text
        {
            get => _metroSwitch.Text;
            set => _metroSwitch.Text = value;
        }

        public bool Switched
        {
            get => _metroSwitch.Switched;
            set => _metroSwitch.Switched = value;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection
            {
                new DesignerActionHeaderItem("Metro"),
                new DesignerActionPropertyItem("MetroStyleManager", "MetroStyleManager", "Metro", "Gets or sets the stylemanager for the control."),
                new DesignerActionPropertyItem("Style", "Style", "Metro", "Gets or sets the style."),

                new DesignerActionHeaderItem("Informations"),
                new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
                new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

                new DesignerActionHeaderItem("Appearance"),
                new DesignerActionPropertyItem("Text", "Text", "Appearance", "Gets or sets the The text associated with the control."),
                new DesignerActionPropertyItem("Switched", "Switched", "Appearance", "Gets or sets a value indicating whether the control is switched."),
            };

            return items;
        }
    }

    #endregion
}