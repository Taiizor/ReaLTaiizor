#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

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
            get => _ShowText;
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
            Size = new(222, 111);
            Font = ThemeLost.TitleFont;
            BackColor = ThemeLost.ForeBrush.Color;
            ForeColor = ThemeLost.FontBrush.Color;
            Padding = new Padding(5);
        }

        public override void DrawShadow(Graphics g)
        {
            for (int i = 0; i < ThemeLost.ShadowSize; i++)
            {
                g.DrawRectangle(new(ThemeLost.ShadowColor.Shade(ThemeLost.ShadowSize, i)), ShadeRect(i));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            if (_ShowText)
            {
                e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 2, 2);
            }

            foreach (Control c in Controls)
            {
                if (c is ControlLostBase)
                {
                    (c as ControlLostBase).DrawShadow(e.Graphics);
                }
            }

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
            {
                if (c is ControlLostBase && (c as ControlLostBase).ShadowLevel != 0)
                {
                    (c as ControlLostBase).Invalidate();
                }
            }
        }
    }

    #endregion
}