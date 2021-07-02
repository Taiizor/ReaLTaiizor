#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroProgressBarActionListAction

    internal class MetroProgressBarActionList : DesignerActionList
    {
        private readonly MetroProgressBar _metroProgressBar;

        public MetroProgressBarActionList(IComponent component) : base(component)
        {
            _metroProgressBar = (MetroProgressBar)component;
        }

        public Style Style
        {
            get => _metroProgressBar.Style;
            set => _metroProgressBar.Style = value;
        }

        public string ThemeAuthor => _metroProgressBar.ThemeAuthor;

        public string ThemeName => _metroProgressBar.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroProgressBar.StyleManager;
            set => _metroProgressBar.StyleManager = value;
        }

        public int Value
        {
            get => _metroProgressBar.Value;
            set => _metroProgressBar.Value = value;
        }

        public int Maximum
        {
            get => _metroProgressBar.Maximum;
            set => _metroProgressBar.Maximum = value;
        }

        public int Minimum
        {
            get => _metroProgressBar.Minimum;
            set => _metroProgressBar.Minimum = value;
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
                new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the minimum value of the progressbar."),
                new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the maximum value of the progressbar."),
                new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets the current position of the progressbar."),
            };
            return items;
        }
    }

    #endregion
}