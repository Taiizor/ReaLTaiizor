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
    #region MetroEllipseActionListAction

    public class MetroEllipseActionList : DesignerActionList
    {
        private readonly MetroEllipse _metroEllipse;

        public MetroEllipseActionList(IComponent component) : base(component)
        {
            _metroEllipse = (MetroEllipse)component;
        }

        public Style Style
        {
            get => _metroEllipse.Style;
            set => _metroEllipse.Style = value;
        }

        public string ThemeAuthor => _metroEllipse.ThemeAuthor;

        public string ThemeName => _metroEllipse.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroEllipse.StyleManager;
            set => _metroEllipse.StyleManager = value;
        }

        public string Text
        {
            get => _metroEllipse.Text;
            set => _metroEllipse.Text = value;
        }

        public Font Font
        {
            get => _metroEllipse.Font;
            set => _metroEllipse.Font = value;
        }

        public int BorderThickness
        {
            get => _metroEllipse.BorderThickness;
            set => _metroEllipse.BorderThickness = value;
        }
        public Size ImageSize
        {
            get => _metroEllipse.ImageSize;
            set => _metroEllipse.ImageSize = value;
        }
        public Image Image
        {
            get => _metroEllipse.Image;
            set => _metroEllipse.Image = value;
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
                new DesignerActionPropertyItem("BorderThickness", "BorderThickness", "Appearance", "Gets or sets the border thickness associated with the control."),
                new DesignerActionPropertyItem("Image", "Image", "Appearance", "Gets or sets the image associated with the control."),
                new DesignerActionPropertyItem("ImageSize", "ImageSize", "Appearance", "Gets or sets the image size associated with the control."),
            };
            return items;
        }
    }

    #endregion
}