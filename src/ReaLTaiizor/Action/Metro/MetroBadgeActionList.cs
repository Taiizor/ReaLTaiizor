#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroBadgeActionListAction

    public class MetroBadgeActionList : DesignerActionList
    {
        private readonly MetroBadge _metroBadge;

        public MetroBadgeActionList(IComponent component) : base(component)
        {
            _metroBadge = (MetroBadge)component;
        }

        public Style Style
        {
            get => _metroBadge.Style;
            set => _metroBadge.Style = value;
        }

        public string ThemeAuthor => _metroBadge.ThemeAuthor;

        public string ThemeName => _metroBadge.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroBadge.StyleManager;
            set => _metroBadge.StyleManager = value;
        }

        public string Text
        {
            get => _metroBadge.Text;
            set => _metroBadge.Text = value;
        }

        public Font Font
        {
            get => _metroBadge.Font;
            set => _metroBadge.Font = value;
        }

        public BadgeAlign BadgeAlignment
        {
            get => _metroBadge.BadgeAlignment;
            set => _metroBadge.BadgeAlignment = value;
        }

        public string BadgeText
        {
            get => _metroBadge.BadgeText;
            set => _metroBadge.BadgeText = value;
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

                new DesignerActionHeaderItem("Badge"),
                new DesignerActionPropertyItem("BadgeText", "BadgeText", "Badge", "Gets or sets the badge text associated with the control."),
                new DesignerActionPropertyItem("BadgeAlignment", "BadgeAlignment", "Badge", "Gets or sets the badge alignment associated with the control.")
            };
            return items;
        }
    }

    #endregion
}