#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Child.Crown
{
    #region CrownDropDownItemChild

    public class CrownDropdownItem
    {
        #region Property Region

        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        #endregion

        #region Constructor Region

        public CrownDropdownItem()
        { }

        public CrownDropdownItem(string text)
        {
            Text = text;
        }

        public CrownDropdownItem(string text, Bitmap icon) : this(text)
        {
            Icon = icon;
        }

        #endregion
    }

    #endregion
}