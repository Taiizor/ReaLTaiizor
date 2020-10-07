#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDockTabDocking

    internal class CrownDockTab
    {
        #region Property Region

        public CrownDockContent DockContent { get; set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle CloseButtonRectangle { get; set; }

        public bool Hot { get; set; }

        public bool CloseButtonHot { get; set; }

        public bool ShowSeparator { get; set; }

        #endregion

        #region Constructor Region

        public CrownDockTab(CrownDockContent content)
        {
            DockContent = content;
        }

        #endregion

        #region Method Region

        public int CalculateWidth(Graphics g, Font font)
        {
            int width = (int)g.MeasureString(DockContent.DockText, font).Width;
            width += 10;

            return width;
        }

        #endregion
    }

    #endregion
}