#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Extensions;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostPanel

    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class LostPanel : ControlLostBase
    {

        #region Properties

        private bool _ShowText = true;
        public bool ShowText
        {
            get { return _ShowText; }
            set
            {
                _ShowText = value;
                Invalidate();
            }
        }

        #endregion

        public LostPanel()
        {
            DoubleBuffered = true;
            Size = new Size(222, 111);
            Font = ThemeLost.TitleFont;
            BackColor = ThemeLost.ForeBrush.Color;
            ForeColor = ThemeLost.FontBrush.Color;
            Padding = new Padding(5);
        }

        public override void DrawShadow(Graphics g)
        {
            for (int i = 0; i < ThemeLost.ShadowSize; i++)
                g.DrawRectangle(new Pen(ThemeLost.ShadowColor.Shade(ThemeLost.ShadowSize, i)), ShadeRect(i));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            if (_ShowText)
                e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 2, 2);

            foreach (Control c in Controls)
                if (c is ControlLostBase)
                    (c as ControlLostBase).DrawShadow(e.Graphics);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            //return;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //return;
            foreach (Control c in Controls)
                if (c is ControlLostBase && (c as ControlLostBase).ShadowLevel != 0)
                    (c as ControlLostBase).Invalidate();
        }
    }

    #endregion
}