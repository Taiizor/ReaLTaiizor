#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Child.Crown
{
    #region CrownDropDownItemChild

    public class CrownDropDownItem
    {
        #region Property Region

        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        #endregion

        #region Constructor Region

        public CrownDropDownItem()
        { }

        public CrownDropDownItem(string text)
        {
            Text = text;
        }

        public CrownDropDownItem(string text, Bitmap icon) : this(text)
        {
            Icon = icon;
        }

        #endregion
    }

    #endregion
}