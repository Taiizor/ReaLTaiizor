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
    #region MetroTileActionListAction

    public class MetroTileActionList : DesignerActionList
    {
        private readonly MetroTile _metroTile;

        public MetroTileActionList(IComponent component) : base(component)
        {
            _metroTile = (MetroTile)component;
        }

        public Style Style
        {
            get => _metroTile.Style;
            set => _metroTile.Style = value;
        }

        public string ThemeAuthor => _metroTile.ThemeAuthor;

        public string ThemeName => _metroTile.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroTile.StyleManager;
            set => _metroTile.StyleManager = value;
        }

        public string Text
        {
            get => _metroTile.Text;
            set => _metroTile.Text = value;
        }

        public Font Font
        {
            get => _metroTile.Font;
            set => _metroTile.Font = value;
        }

        public Image BackgroundImage
        {
            get => _metroTile.BackgroundImage;
            set => _metroTile.BackgroundImage = value;
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
                new DesignerActionPropertyItem("BackgroundImage", "BackgroundImage", "Appearance", "Gets or sets the BackgroundImage associated with the control."),
            };
            return items;
        }
    }

    #endregion
}