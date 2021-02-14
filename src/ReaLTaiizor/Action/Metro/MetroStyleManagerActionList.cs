#region Imports

using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroStyleManagerActionListAction

    internal class MetroStyleManagerActionList : DesignerActionList
    {
        private readonly MetroStyleManager _styleManger;

        public MetroStyleManagerActionList(IComponent component) : base(component)
        {
            _styleManger = (MetroStyleManager)component;
        }

        public Style Style
        {
            get => _styleManger.Style;
            set => _styleManger.Style = value;
        }

        public string ThemeAuthor => _styleManger.ThemeAuthor;

        public string ThemeName => _styleManger.ThemeName;

        [Editor(typeof(MetroStyleManager.FileNamesEditor), typeof(UITypeEditor)), Category("Metro"), Description("Gets or sets the custom theme file.")]
        public string CustomTheme
        {
            get => _styleManger.CustomTheme;
            set => _styleManger.CustomTheme = value;
        }

        public Form OwnerForm
        {
            get => _styleManger.OwnerForm;
            set => _styleManger.OwnerForm = value;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new()
            {
                new DesignerActionHeaderItem("Metro"),
                new DesignerActionPropertyItem("OwnerForm", "OwnerForm", "Metro", "Gets or sets the form (MetroForm) to Apply themes for."),
                new DesignerActionPropertyItem("Style", "Style", "Metro", "Gets or sets the style."),
                new DesignerActionPropertyItem("CustomTheme", "CustomTheme", "Metro", "Gets or sets the custom theme file."),

                new DesignerActionHeaderItem("Information"),
                new DesignerActionPropertyItem("ThemeName", "ThemeName", "Information", "Gets or sets the The Theme name associated with the theme."),
                new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Information", "Gets or sets the The Author name associated with the theme."),
            };
            return items;
        }
    }

    #endregion
}