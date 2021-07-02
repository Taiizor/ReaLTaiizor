#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroTrackBarActionList.csAction

    public class MetroTrackBarActionList : DesignerActionList
    {
        private readonly MetroTrackBar _metroTrackBar;

        public MetroTrackBarActionList(IComponent component) : base(component)
        {
            _metroTrackBar = (MetroTrackBar)component;
        }

        public Style Style
        {
            get => _metroTrackBar.Style;
            set => _metroTrackBar.Style = value;
        }

        public string ThemeAuthor => _metroTrackBar.ThemeAuthor;

        public string ThemeName => _metroTrackBar.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroTrackBar.StyleManager;
            set => _metroTrackBar.StyleManager = value;
        }

        public int Maximum
        {
            get => _metroTrackBar.Maximum;
            set => _metroTrackBar.Maximum = value;
        }

        public int Minimum
        {
            get => _metroTrackBar.Minimum;
            set => _metroTrackBar.Minimum = value;
        }

        public int Value
        {
            get => _metroTrackBar.Value;
            set => _metroTrackBar.Value = value;
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
                new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the upper limit of the range this TrackBar is working with."),
                new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the lower limit of the range this TrackBar is working with."),
                new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets a numeric value that represents the current position of the scroll box on the track bar."),
            };
            return items;
        }
    }

    #endregion
}