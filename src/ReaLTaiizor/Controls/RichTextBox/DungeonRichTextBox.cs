#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonRichTextBox

    [DefaultEvent("TextChanged")]
    public class DungeonRichTextBox : Control
    {

        #region Variables

        public RichTextBox DungeonRTB = new();
        private bool _ReadOnly;
        private bool _WordWrap;
        private bool _AutoWordSelection;
        private GraphicsPath Shape;
        private Pen P1;

        #endregion
        #region Properties

        public Color BorderColor { get; set; } = Color.FromArgb(180, 180, 180);

        public Color EdgeColor { get; set; } = Color.White;

        public Color TextBackColor { get; set; } = Color.White;

        public override string Text
        {
            get => DungeonRTB.Text;
            set
            {
                DungeonRTB.Text = value;
                Invalidate();
            }
        }
        public bool ReadOnly
        {
            get => _ReadOnly;
            set
            {
                _ReadOnly = value;
                if (DungeonRTB != null)
                {
                    DungeonRTB.ReadOnly = value;
                }
            }
        }
        public bool WordWrap
        {
            get => _WordWrap;
            set
            {
                _WordWrap = value;
                if (DungeonRTB != null)
                {
                    DungeonRTB.WordWrap = value;
                }
            }
        }
        public bool AutoWordSelection
        {
            get => _AutoWordSelection;
            set
            {
                _AutoWordSelection = value;
                if (DungeonRTB != null)
                {
                    DungeonRTB.AutoWordSelection = value;
                }
            }
        }
        #endregion
        #region EventArgs

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);
            DungeonRTB.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            DungeonRTB.Font = Font;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            DungeonRTB.Size = new(Width - 13, Height - 11);
        }

        private void _Enter(object Obj, EventArgs e)
        {
            P1 = new(Color.FromArgb(205, 87, 40));
            Refresh();
        }

        private void _Leave(object Obj, EventArgs e)
        {
            P1 = new(Color.FromArgb(180, 180, 180));
            Refresh();
        }

        protected override void OnResize(System.EventArgs e)
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
            DungeonRTB.Text = Text;
        }

        #endregion

        public void AddRichTextBox()
        {
            RichTextBox _RTB = DungeonRTB;
            _RTB.BackColor = TextBackColor;
            _RTB.Size = new(Width - 10, 100);
            _RTB.Location = new(7, 5);
            _RTB.Text = string.Empty;
            _RTB.BorderStyle = BorderStyle.None;
            _RTB.Font = Font;
            _RTB.Multiline = true;
        }

        public DungeonRichTextBox() : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddRichTextBox();
            Controls.Add(DungeonRTB);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(76, 76, 76);

            P1 = new(BorderColor);
            Text = null;
            Font = new("Tahoma", 10);
            Size = new(150, 100);
            WordWrap = true;
            AutoWordSelection = false;
            DoubleBuffered = true;

            DungeonRTB.Enter += _Enter;
            DungeonRTB.Leave += _Leave;
            TextChanged += _TextChanged;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(BackColor);
            G.FillPath(new SolidBrush(EdgeColor), Shape);
            G.DrawPath(P1, Shape);
            G.Dispose();
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            B.Dispose();
        }
    }

    #endregion
}