#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroControlBoxActionListAction

    internal class MetroControlBoxActionList : DesignerActionList
    {
        private readonly MetroControlBox _metroControBox;

        public MetroControlBoxActionList(IComponent component) : base(component)
        {
            _metroControBox = (MetroControlBox)component;
        }

        public Style Style
        {
            get => _metroControBox.Style;
            set => _metroControBox.Style = value;
        }

        public string ThemeAuthor => _metroControBox.ThemeAuthor;

        public string ThemeName => _metroControBox.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroControBox.StyleManager;
            set => _metroControBox.StyleManager = value;
        }

        public bool MaximizeBox
        {
            get => _metroControBox.MaximizeBox;
            set => _metroControBox.MaximizeBox = value;
        }

        public bool MinimizeBox
        {
            get => _metroControBox.MinimizeBox;
            set => _metroControBox.MinimizeBox = value;
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
                new DesignerActionPropertyItem("MaximizeBox", "Enable MaximizeBox", "Appearance", "Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form."),
                new DesignerActionPropertyItem("MinimizeBox", "Enable MinimizeBox", "Appearance", "Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")
            };
            return items;
        }
    }

    #endregion
}