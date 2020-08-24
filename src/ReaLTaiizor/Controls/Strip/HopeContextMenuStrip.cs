#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls.Strip
{
    #region HopeContextMenuStrip

    public class HopeContextMenuStrip : ContextMenuStrip
    {
        public HopeContextMenuStrip()
        {
            Renderer = new HopeBase.ToolStripRender();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.White;
        }
    }

    #endregion
}