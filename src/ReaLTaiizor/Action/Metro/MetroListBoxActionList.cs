#region Imports

using ReaLTaiizor.Child.Metro;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroListBoxActionListAction

    internal class MetroListBoxActionList : DesignerActionList
    {
        private readonly MetroListBox _metroListBox;

        public MetroListBoxActionList(IComponent component) : base(component)
        {
            _metroListBox = (MetroListBox)component;
        }

        public Style Style
        {
            get => _metroListBox.Style;
            set => _metroListBox.Style = value;
        }

        public string ThemeAuthor => _metroListBox.ThemeAuthor;

        public string ThemeName => _metroListBox.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroListBox.StyleManager;
            set => _metroListBox.StyleManager = value;
        }

        [TypeConverter(typeof(CollectionConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public MetroItemCollection Items => _metroListBox.Items;

        public int ItemHeight
        {
            get => _metroListBox.ItemHeight;
            set => _metroListBox.ItemHeight = value;
        }

        public bool MultiSelect
        {
            get => _metroListBox.MultiSelect;
            set => _metroListBox.MultiSelect = value;
        }

        public bool ShowScrollBar
        {
            get => _metroListBox.ShowScrollBar;
            set => _metroListBox.ShowScrollBar = value;
        }

        public bool ShowBorder
        {
            get => _metroListBox.ShowBorder;
            set => _metroListBox.ShowBorder = value;
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
                new DesignerActionPropertyItem("Items", "Items", "Appearance", "Gets the items of the ListBox."),
                new DesignerActionPropertyItem("ItemHeight", "ItemHeight", "Appearance", "Gets or sets the height of an item in the ListBox."),
                new DesignerActionPropertyItem("MultiSelect", "MultiSelect", "Appearance", "Gets or sets a value indicating whether the ListBox supports multiple rows."),
                new DesignerActionPropertyItem("ShowScrollBar", "ShowScrollBar", "Appearance", "Gets or sets a value indicating whether the vertical scroll bar is shown or not."),
                new DesignerActionPropertyItem("ShowBorder", "ShowBorder", "Appearance", "Gets or sets a value indicating whether the border shown or not."),
            };
            return items;
        }
    }

    #endregion
}