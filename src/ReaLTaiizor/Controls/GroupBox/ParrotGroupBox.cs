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
        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the border")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The width of the border")]
        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the text of the groupbox")]
        public bool ShowText
        {
            get => showText;
            set
            {
                showText = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (showText)
            {
                groupName.BackColor = Color.Transparent;
                groupName.Text = Text;
                groupName.Font = Font;
                groupName.Location = new Point(9, 0);
                groupName.AutoSize = true;
                groupName.ForeColor = textColor;
                base.Controls.Add(groupName);
                e.Graphics.DrawLine(new Pen(borderColor, borderWidth), 1, 6, 6, 6);
                e.Graphics.DrawLine(new Pen(borderColor, borderWidth), base.Width - 2, 6, groupName.Location.X + groupName.Width, 6);
                e.Graphics.DrawLine(new Pen(borderColor, borderWidth), 1, base.Height - 2, base.Width - 2, base.Height - 2);
                e.Graphics.DrawLine(new Pen(borderColor, borderWidth), 1, 6, 1, base.Height - 2);
                e.Graphics.DrawLine(new Pen(borderColor, borderWidth), base.Width - 2, 6, base.Width - 2, base.Height - 2);
                return;
            }
            base.Controls.Remove(groupName);
            e.Graphics.DrawLine(new Pen(borderColor, borderWidth), 1, 1, base.Width - 2, 1);
            e.Graphics.DrawLine(new Pen(borderColor, borderWidth), 1, base.Height - 2, base.Width - 2, base.Height - 2);
            e.Graphics.DrawLine(new Pen(borderColor, borderWidth), 1, 1, 1, base.Height - 2);
            e.Graphics.DrawLine(new Pen(borderColor, borderWidth), base.Width - 2, 1, base.Width - 2, base.Height - 2);
        }

        private readonly Label groupName = new();

        private Color borderColor = Color.DodgerBlue;

        private Color textColor = Color.DodgerBlue;

        private int borderWidth = 1;

        private bool showText = true;
    }

    #endregion
}