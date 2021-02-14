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
    #region MetroDefaultButtonActionListAction

    public class MetroDefaultButtonActionList : DesignerActionList
    {
        private readonly MetroDefaultButton _metroButton;

        public MetroDefaultButtonActionList(IComponent component) : base(component)
        {
            _metroButton = (MetroDefaultButton)component;
        }

        public Style Style
        {
            get => _metroButton.Style;
            set => _metroButton.Style = value;
        }

        public string ThemeAuthor => _metroButton.ThemeAuthor;

        public string ThemeName => _metroButton.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroButton.StyleManager;
            set => _metroButton.StyleManager = value;
        }

        public string Text
        {
            get => _metroButton.Text;
            set => _metroButton.Text = value;
        }

        public Font Font
        {
            get => _metroButton.Font;
            set => _metroButton.Font = value;
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