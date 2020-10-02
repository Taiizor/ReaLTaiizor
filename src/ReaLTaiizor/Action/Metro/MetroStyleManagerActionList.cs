#region Imports

using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using ReaLTaiizor.Enum.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
	#region MetroStyleManagerActionListAction

	class MetroStyleManagerActionList : DesignerActionList
	{
		private readonly MetroStyleManager _metroStyleManger;

		public MetroStyleManagerActionList(IComponent component) : base(component)
		{
			_metroStyleManger = (MetroStyleManager)component;
		}

		public Style Style
		{
			get => _metroStyleManger.Style;
			set => _metroStyleManger.Style = value;
		}

		public string ThemeAuthor => _metroStyleManger.ThemeAuthor;

		public string ThemeName => _metroStyleManger.ThemeName;

		[Editor(typeof(MetroStyleManager.FileNamesEditor), typeof(UITypeEditor)), Category("Metro"), Description("Gets or sets the custom theme file.")]
		public string CustomTheme
		{
			get => _metroStyleManger.CustomTheme;
			set => _metroStyleManger.CustomTheme = value;
		}

		public Form OwnerForm
		{
			get => _metroStyleManger.MetroForm;
			set => _metroStyleManger.MetroForm = value;
		}

		public override DesignerActionItemCollection GetSortedActionItems()
		{
			var items = new DesignerActionItemCollection
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