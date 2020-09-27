#region Imports

using System.Drawing;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroLinkLabelActionListAction

    class MetroLinkLabelActionList : DesignerActionList
    {
        private readonly MetroLinkLabel _metroLinkLabel;

        public MetroLinkLabelActionList(IComponent component) : base(component)
        {
            _metroLinkLabel = (MetroLinkLabel)component;
        }

        public Style Style
        {
            get => _metroLinkLabel.Style;
            set => _metroLinkLabel.Style = value;
        }

        public string ThemeAuthor => _metroLinkLabel.ThemeAuthor;

        public string ThemeName => _metroLinkLabel.ThemeName;

        public MetroStyleManager MetroStyleManager
        {
            get => _metroLinkLabel.MetroStyleManager;
            set => _metroLinkLabel.MetroStyleManager = value;
        }

        public string Text
        {
            get => _metroLinkLabel.Text;
            set => _metroLinkLabel.Text = value;
        }

        public Font Font
        {
            get => _metroLinkLabel.Font;
            set => _metroLinkLabel.Font = value;
        }

        public LinkBehavior LinkBehaviour
        {
            get => _metroLinkLabel.LinkBehavior;
            set => _metroLinkLabel.LinkBehavior = value;
        }

        public Color LinkColor
        {
            get => _metroLinkLabel.LinkColor;
            set => _metroLinkLabel.LinkColor = value;
        }

        public Color ActiveLinkColor
        {
            get => _metroLinkLabel.ActiveLinkColor;
            set => _metroLinkLabel.ActiveLinkColor = value;
        }

        public Color VisitedLinkColor
        {
            get => _metroLinkLabel.VisitedLinkColor;
            set => _metroLinkLabel.VisitedLinkColor = value;
        }

        public bool LinkVisited
        {
            get => _metroLinkLabel.LinkVisited;
            set => _metroLinkLabel.LinkVisited = value;
        }

        public LinkLabel.LinkCollection Links => _metroLinkLabel.Links;

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
                new DesignerActionPropertyItem("Font", "Font", "Appearance", "Gets or sets the The font associated with the control."),
                new DesignerActionPropertyItem("LinkVisited", "LinkVisited", "Appearance", "Gets or sets a value indicating whether a link should be displayed as though it were visited."),
                new DesignerActionPropertyItem("LinkColor", "LinkColor", "Appearance", "Gets or sets the color used when displaying a normal link."),
                new DesignerActionPropertyItem("ActiveLinkColor", "ActiveLinkColor", "Appearance", "Gets or sets the color used to display an active link."),
                new DesignerActionPropertyItem("VisitedLinkColor", "VisitedLinkColor", "Appearance", "Gets or sets the color used when displaying a link that that has been previously visited."),

                new DesignerActionHeaderItem("Behaviour"),
                new DesignerActionPropertyItem("LinkBehaviour", "LinkBehaviour", "Behaviour", "Gets or sets a value that represents the behavior of a link."),
                new DesignerActionPropertyItem("Links", "Links", "Behaviour", "Gets the collection of links contained within the LinkLabel.")
            };

            return items;
        }
    }

    #endregion
}