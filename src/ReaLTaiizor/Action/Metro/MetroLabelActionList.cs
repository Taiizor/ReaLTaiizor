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
    #region MetroLabelActionListAction

    internal class MetroLabelActionList : DesignerActionList
    {
        private readonly MetroLabel _metroLabel;

        public MetroLabelActionList(IComponent component) : base(component)
        {
            _metroLabel = (MetroLabel)component;
        }

        public Style Style
        {
            get => _metroLabel.Style;
            set => _metroLabel.Style = value;
        }

        public string ThemeAuthor => _metroLabel.ThemeAuthor;

        public string ThemeName => _metroLabel.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroLabel.StyleManager;
            set => _metroLabel.StyleManager = value;
        }

        public string Text
        {
            get => _metroLabel.Text;
            set => _metroLabel.Text = value;
        }

        public Font Font
        {
            get => _metroLabel.Font;
            set => _metroLabel.Font = value;
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
                new DesignerActionPropertyItem("Font", "Font", "Appearance", "Gets or sets the The font associated with the control.")
            };
            return items;
        }
    }

    #endregion
}