﻿#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region DungeonLinkLabel

    public class DungeonLinkLabel : LinkLabel
    {
        public DungeonLinkLabel()
        {
            Font = new Font("Segoe UI", 11, FontStyle.Regular);
            BackColor = Color.Transparent;
            LinkColor = Color.FromArgb(240, 119, 70);
            ActiveLinkColor = Color.FromArgb(221, 72, 20);
            VisitedLinkColor = Color.FromArgb(240, 119, 70);
            LinkBehavior = LinkBehavior.AlwaysUnderline;
        }
    }

    #endregion
}