#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RichTextBoxEdit

    [DefaultEvent("TextChanged")]
    public class RichTextBoxEdit : Control
    {

        #region Variables

        public RichTextBox RT_RTB = new();
        private bool _ReadOnly;
        private bool _WordWrap;
        private bool _AutoWordSelection;
        private GraphicsPath Shape;
        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        private Color _BaseColor = Color.Transparent;
        private Color _EdgeColor = Color.White;
        private Color _BorderColor = Color.FromArgb(180, 180, 180);
        private Color _TextBackColor = Color.White;
        private Font _TextFont = new("Tahoma", 10);
        private BorderStyle _TextBorderStyle = BorderStyle.None;

        #endregion
        #region Properties

        public override string Text
        {
            get => RT_RTB.Text;
            set
            {
                RT_RTB.Text = value;
                Invalidate();
            }
        }

        public bool ReadOnly
        {
            get => _ReadOnly;
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
            get => _WordWrap;
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
            get => _AutoWordSelection;
            set
            {
                _AutoWordSelection = value;
                if (RT_RTB != null)
                {
                    RT_RTB.AutoWordSelection = value;
                }
            }
        }

        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        public Color EdgeColor
        {
            get => _EdgeColor;
            set
            {
                _EdgeColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        public Color TextBackColor
        {
            get => _TextBackColor;
            set
            {
                _TextBackColor = value;
                Invalidate();
            }
        }

        public Font TextFont
        {
            get => _TextFont;
            set
            {
                _TextFont = value;
                Invalidate();
            }
        }

        public BorderStyle TextBorderStyle
        {
            get => _TextBorderStyle;
            set
            {
                _TextBorderStyle = value;
                Invalidate();
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
            RT_RTB.Size = new(Width - 13, Height - 11);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Shape = new();
            GraphicsPath _Shape = Shape;
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
            RichTextBox _RTB = RT_RTB;
            _RTB.BackColor = TextBackColor;
            _RTB.Size = new(Width - 10, 100);
            _RTB.Location = new(7, 5);
            _RTB.Text = string.Empty;
            _RTB.BorderStyle = TextBorderStyle;
            _RTB.Font = TextFont;
            _RTB.Multiline = true;
        }

        public RichTextBoxEdit() : base()
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddRichTextBox();
            Controls.Add(RT_RTB);
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;

            Text = null;
            Font = new("Tahoma", 10);
            Size = new(150, 100);
            WordWrap = true;
            AutoWordSelection = false;
            DoubleBuffered = true;

            TextChanged += _TextChanged;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingType;
            G.Clear(BaseColor);
            G.FillPath(new SolidBrush(EdgeColor), Shape);
            G.DrawPath(new(BorderColor), Shape);
            G.Dispose();
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            B.Dispose();
        }
    }

    #endregion
}