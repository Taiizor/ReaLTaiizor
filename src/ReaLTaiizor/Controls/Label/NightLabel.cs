#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightLabel

    public class NightLabel : Label
    {
        public NightLabel()
        {
            Font = new Font("Segoe UI", 9, FontStyle.Regular);
            BackColor = Color.Transparent;
            ForeColor = ColorTranslator.FromHtml("#72767F");
            Cursor = Cursors.Arrow;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }
    }

    #endregion
}