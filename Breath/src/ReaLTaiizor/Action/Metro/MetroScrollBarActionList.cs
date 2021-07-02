#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroScrollBarActionListAction

    internal class MetroScrollBarActionList : DesignerActionList
    {
        private readonly MetroScrollBar _metroScrollBar;

        public MetroScrollBarActionList(IComponent component) : base(component)
        {
            _metroScrollBar = (MetroScrollBar)component;
        }

        public Style Style
        {
            get => _metroScrollBar.Style;
            set => _metroScrollBar.Style = value;
        }

        public string ThemeAuthor => _metroScrollBar.ThemeAuthor;

        public string ThemeName => _metroScrollBar.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroScrollBar.StyleManager;
            set => _metroScrollBar.StyleManager = value;
        }

        public int Maximum
        {
            get => _metroScrollBar.Maximum;
            set => _metroScrollBar.Maximum = value;
        }

        public int Minimum
        {
            get => _metroScrollBar.Minimum;
            set => _metroScrollBar.Minimum = value;
        }


        public int Value
        {
            get => _metroScrollBar.Value;
            set => _metroScrollBar.Value = value;
        }


        public int SmallChange
        {
            get => _metroScrollBar.SmallChange;
            set => _metroScrollBar.SmallChange = value;
        }


        public int LargeChange
        {
            get => _metroScrollBar.LargeChange;
            set => _metroScrollBar.LargeChange = value;
        }

        public ScrollOrientate Orientation
        {
            get => _metroScrollBar.Orientation;
            set => _metroScrollBar.Orientation = value;
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
                new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Gets or sets the scroll bar orientation."),

                new DesignerActionHeaderItem("Behavior"),
                new DesignerActionPropertyItem("Maximum", "Maximum", "Behavior", "Gets or sets the upper limit of the scrollable range."),
                new DesignerActionPropertyItem("Minimum", "Minimum", "Behavior", "Gets or sets the lower limit of the scrollable range."),
                new DesignerActionPropertyItem("Value", "Value", "Behavior", "Gets or sets a numeric value that represents the current position of the scroll bar box."),
                new DesignerActionPropertyItem("LargeChange", "LargeChange", "Behavior", "Gets or sets the distance to move a scroll bar in response to a large scroll command."),
                new DesignerActionPropertyItem("SmallChange", "SmallChange", "Behavior", "Gets or sets the distance to move a scroll bar in response to a small scroll command."),

            };
            return items;
        }
    }

    #endregion
}