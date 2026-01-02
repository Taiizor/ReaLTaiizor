#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotGroupBox

    public class ParrotGroupBox : System.Windows.Forms.GroupBox
    {
        public ParrotGroupBox()
        {
            Controls.Add(groupName);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the border")]
        public Color BorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DodgerBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text")]
        public Color TextColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DodgerBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The width of the border")]
        public int BorderWidth
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 1;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the text of the groupbox")]
        public bool ShowText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            groupName.Visible = ShowText;

            if (ShowText)
            {
                groupName.BackColor = Color.Transparent;

                groupName.Text = Text;
                groupName.Font = Font;
                groupName.Location = new Point(9, 0);
                groupName.AutoSize = true;
                groupName.ForeColor = TextColor;

                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), 1, 6, 6, 6);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), base.Width - 2, 6, groupName.Location.X + groupName.Width, 6);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), 1, base.Height - 2, base.Width - 2, base.Height - 2);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), 1, 6, 1, base.Height - 2);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), base.Width - 2, 6, base.Width - 2, base.Height - 2);
            }
            else
            {
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), 1, 1, base.Width - 2, 1);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), 1, base.Height - 2, base.Width - 2, base.Height - 2);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), 1, 1, 1, base.Height - 2);
                e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), base.Width - 2, 1, base.Width - 2, base.Height - 2);
            }
        }

        private readonly Label groupName = new();
    }

    #endregion
}