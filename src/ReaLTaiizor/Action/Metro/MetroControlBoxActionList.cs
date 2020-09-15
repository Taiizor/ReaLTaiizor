#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Controls;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroControlBoxActionListAction

    internal class MetroControlBoxActionList : DesignerActionList
    {
        private readonly MetroControlBox _metroControlBox;

        public MetroControlBoxActionList(IComponent component) : base(component)
        {
            _metroControlBox = (MetroControlBox)component;
        }

        public Style Style
        {
            get => _metroControlBox.Style;
            set => _metroControlBox.Style = value;
        }

        public string ThemeAuthor => _metroControlBox.ThemeAuthor;

        public string ThemeName => _metroControlBox.ThemeName;

        public MetroStyleManager MetroStyleManager
        {
            get => _metroControlBox.MetroStyleManager;
            set => _metroControlBox.MetroStyleManager = value;
        }

        public bool MaximizeBox
        {
            get => _metroControlBox.MaximizeBox;
            set => _metroControlBox.MaximizeBox = value;
        }

        public bool MinimizeBox
        {
            get => _metroControlBox.MinimizeBox;
            set => _metroControlBox.MinimizeBox = value;
        }

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
                new DesignerActionPropertyItem("MaximizeBox", "Enable MaximizeBox", "Appearance", "Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form."),
                new DesignerActionPropertyItem("MinimizeBox", "Enable MinimizeBox", "Appearance", "Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")
            };

            return items;
        }
    }

    #endregion
}