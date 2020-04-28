#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region RichTextEdit

    [DefaultEvent("TextChanged")]
    public class RichTextEdit : Control
    {

        #region Variables

        public RichTextBox RT_RTB = new RichTextBox();
        private bool _ReadOnly;
        private bool _WordWrap;
        private bool _AutoWordSelection;
        private GraphicsPath Shape;

        #endregion
        #region Properties

        public override string Text
        {
            get { return RT_RTB.Text; }
            set
            {
                RT_RTB.Text = value;
                Invalidate();
            }
        }
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                if (RT_RTB != null)
                {
                    RT_RTB.ReadOnly = value;
                }
            }
        }
        public bool WordWrap
        {
            get { return _WordWrap; }
            set
            {
                _WordWrap = value;
                if (RT_RTB != null)
                {
                    RT_RTB.WordWrap = value;
                }
            }
        }
        public bool AutoWordSelection
        {
            get { return _AutoWordSelection; }
            set
            {
                _AutoWordSelection = value;
                if (RT_RTB != null)
                {
                    RT_RTB.AutoWordSelection = value;
                }
            }
        }
        #endregion
        #region EventArgs

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            RT_RTB.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RT_RTB.Font = Font;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RT_RTB.Size = new Size(Width - 13, Height - 11);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Shape = new GraphicsPath();
            var _Shape = Shape;
            _Shape.AddArc(0, 0, 10, 10, 180, 90);
            _Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            _Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            _Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            _Shape.CloseAllFigures();
        }

        public void _TextChanged(object s, EventArgs e)
        {
            RT_RTB.Text = Text;
        }

        #endregion

        public void AddRichTextBox()
        {
            var _RTB = RT_RTB;
            _RTB.BackColor = Color.White;
            _RTB.Size = new Size(Width - 10, 100);
            _RTB.Location = new Point(7, 5);
            _RTB.Text = string.Empty;
            _RTB.BorderStyle = BorderStyle.None;
            _RTB.Font = new Font("Tahoma", 10);
            _RTB.Multiline = true;
        }

        public RichTextEdit() : base()
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddRichTextBox();
            Controls.Add(RT_RTB);
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;

            Text = null;
            Font = new Font("Tahoma", 10);
            Size = new Size(150, 100);
            WordWrap = true;
            AutoWordSelection = false;
            DoubleBuffered = true;

            TextChanged += _TextChanged;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(this.Width, this.Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(Color.Transparent);
            G.FillPath(Brushes.White, this.Shape);
            G.DrawPath(new Pen(Color.FromArgb(180, 180, 180)), this.Shape);
            G.Dispose();
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            B.Dispose();
        }
    }

    #endregion
}