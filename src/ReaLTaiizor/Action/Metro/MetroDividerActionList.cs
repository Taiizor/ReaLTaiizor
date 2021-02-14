#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroDividerActionListAction

    internal class MetroDividerActionList : DesignerActionList
    {
        private readonly MetroDivider _metroDivider;

        public MetroDividerActionList(IComponent component) : base(component)
        {
            _metroDivider = (MetroDivider)component;
        }

        public Style Style
        {
            get => _metroDivider.Style;
            set => _metroDivider.Style = value;
        }

        public string ThemeAuthor => _metroDivider.ThemeAuthor;

        public string ThemeName => _metroDivider.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroDivider.StyleManager;
            set => _metroDivider.StyleManager = value;
        }

        public DividerStyle Orientation
        {
            get => _metroDivider.Orientation;
            set => _metroDivider.Orientation = value;
        }

        public int Thickness
        {
            get => _metroDivider.Thickness;
            set => _metroDivider.Thickness = value;
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
                new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Gets or sets Orientation of the control."),
                new DesignerActionPropertyItem("Thickness", "Thickness", "Appearance", "Gets or sets the divider thickness."),
            };
            return items;
        }
    }

    #endregion
}