#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LinkLabelEdit

    public class LinkLabelEdit : LinkLabel
    {
        public LinkLabelEdit()
        {
            Font = new("Microsoft Sans Serif", 9, FontStyle.Regular);
            BackColor = Color.Transparent;
            LinkColor = Color.FromArgb(32, 34, 37);
            ActiveLinkColor = Color.FromArgb(153, 34, 34);
            VisitedLinkColor = Color.FromArgb(32, 34, 37);
            LinkBehavior = LinkBehavior.NeverUnderline;
        }
    }

    #endregion
}