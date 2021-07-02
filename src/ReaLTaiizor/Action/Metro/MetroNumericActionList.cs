#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroNumericActionListAction

    internal class MetroNumericActionList : DesignerActionList
    {
        private readonly MetroNumeric _metroNumeric;

        public MetroNumericActionList(IComponent component) : base(component)
        {
            _metroNumeric = (MetroNumeric)component;
        }

        public Style Style
        {
            get => _metroNumeric.Style;
            set => _metroNumeric.Style = value;
        }

        public string ThemeAuthor => _metroNumeric.ThemeAuthor;

        public string ThemeName => _metroNumeric.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroNumeric.StyleManager;
            set => _metroNumeric.StyleManager = value;
        }

        public int Maximum
        {
            get => _metroNumeric.Maximum;
            set => _metroNumeric.Maximum = value;
        }

        public int Minimum
        {
            get => _metroNumeric.Minimum;
            set => _metroNumeric.Minimum = value;
        }

        public int Value
        {
            get => _metroNumeric.Value;
            set => _metroNumeric.Value = value;
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
                new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets the current number of the Numeric."),
                new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the minimum number of the Numeric."),
                new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the maximum number of the Numeric."),
            };
            return items;
        }
    }

    #endregion
}