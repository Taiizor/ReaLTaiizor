#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightLinkLabel

    public class NightLinkLabel : LinkLabel
    {
        #region Fields

        private readonly Cursor NativeHand;

        #endregion

        public NightLinkLabel()
        {
            Font = new("Segoe UI", 9, FontStyle.Regular);
            BackColor = Color.Transparent;
            LinkColor = ColorTranslator.FromHtml("#F25D59"); ;
            ActiveLinkColor = ColorTranslator.FromHtml("#DE5954");
            VisitedLinkColor = ColorTranslator.FromHtml("#FE5954");
            LinkBehavior = LinkBehavior.HoverUnderline;
            Cursor = Cursors.Hand;

            NativeHand = new Cursor(NativeMethods.LoadCursor(IntPtr.Zero, NativeConstants.IDC_HAND));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (OverrideCursor == Cursors.Hand)
            {
                OverrideCursor = NativeHand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            OverrideCursor = null;
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
        }
    }

    #endregion
}