#region Imports

using ReaLTaiizor.Animate.Metro;
using ReaLTaiizor.Child.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroTabControl

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroTabControl), "Bitmaps.TabControl.bmp")]
#if NET48_OR_GREATER
    [Designer(typeof(MetroTabControlDesigner))]
#endif
    [ComVisible(true)]
    public class MetroTabControl : TabControl, IMetroControl
    {
        #region Interfaces

        [Category("Metro"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => StyleManager?.Style ?? _style;
            set
            {
                _style = value;
                switch (value)
                {
                    case Style.Light:
                        ApplyTheme();
                        break;
                    case Style.Dark:
                        ApplyTheme(Style.Dark);
                        break;
                    case Style.Custom:
                        ApplyTheme(Style.Custom);
                        break;
                    default:
                        ApplyTheme();
                        break;
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager StyleManager
        {
            get => _styleManager;
            set { _styleManager = value; Invalidate(); }
        }

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private readonly Methods _mth;
        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;
        private readonly PointFAnimate _slideAnimator;
        private Graphics _slideGraphics;
        private Bitmap _slideBitmap;
        private bool _isDerivedStyle = true;
        private bool _useAnimation = true;
        private int _speed = 100;
        private Color _unselectedTextColor;
        private Color _selectedTextColor;
        private TabStyle _tabStyle;

        #endregion Internal Vars

        #region Constructors

        public MetroTabControl()
        {
            SetStyle
            (
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            ItemSize = new(100, 38);
            Font = MetroFonts.UIRegular(8);
            _mth = new Methods();
            _utl = new Utilites();
            _slideAnimator = new PointFAnimate();
            if (Cursor == null)
            {
                Cursor = MCursor;
            }

            MCursor = Cursor;
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            if (!IsDerivedStyle)
            {
                return;
            }

            switch (style)
            {
                case Style.Light:
                    ForegroundColor = Color.FromArgb(65, 177, 225);
                    BackgroundColor = Color.White;
                    UnselectedTextColor = Color.Gray;
                    if (TabStyle == TabStyle.Style1)
                    {
                        SelectedTextColor = Color.White;
                    }
                    else
                    {
                        SelectedTextColor = ForegroundColor;
                    }
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForegroundColor = Color.FromArgb(65, 177, 225);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    UnselectedTextColor = Color.Gray;
                    if (TabStyle == TabStyle.Style1)
                    {
                        SelectedTextColor = Color.White;
                    }
                    else
                    {
                        SelectedTextColor = ForegroundColor;
                    }
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.TabControlDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForegroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "UnselectedTextColor":
                                    UnselectedTextColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SelectedTextColor":
                                    SelectedTextColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                default:
                                    return;
                            }
                        }
                    }

                    UpdateProperties();
                    break;
            }
        }

        private void UpdateProperties()
        {
            try
            {
                InvalidateTabPage(BackgroundColor);
                Invalidate();
                Refresh();
            }
            catch
            {
                //throw new Exception(Ex.StackTrace);
            }
        }

        #endregion ApplyTheme

        #region Properties

        [Category("Metro"), Description("Get or set slide animate time(ms).")]
        public int AnimateTime
        {
            get;
            set;
        } = 200;

        [Category("Metro"), Description("Get or set slide animate easing type")]
        public EasingType AnimateEasingType
        {
            get;
            set;
        } = EasingType.CubeOut;

        [Category("Metro")]
        [Editor(typeof(MetroTabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages => base.TabPages;

        [Category("Metro"), Description("Gets or sets wether the tabcontrol use animation or not.")]
        [DefaultValue(true)]
        public bool UseAnimation
        {
            get => _useAnimation;
            set
            {
                _useAnimation = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the size of the control's tabs.")]
        public new Size ItemSize
        {
            get => base.ItemSize;
            set
            {
                base.ItemSize = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the font used when displaying text in the control.")]
        public new Font Font { get; set; }

        [Category("Metro"), Description("Gets or sets the area of the control (for example, along the top) where the tabs are aligned.")]
        public new TabAlignment Alignment => TabAlignment.Top;

        [Category("Metro"), Description("Gets or sets the speed of transition.")]
        [DefaultValue(20)]
        public int Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                Refresh();
            }
        }

        [Category("Metro")]
        public override DockStyle Dock
        {
            get => base.Dock; set => base.Dock = value;
        }

        [Category("Metro"), Description("Gets or sets the visible of page controls.")]
        public bool ControlsVisible { get; set; } = true;

        [Category("Metro"), Description("Gets or sets the cursor of control.")]
        public Cursor MCursor { get; set; } = Cursors.Hand;

        [Browsable(false)]
        public Cursor Cursor { get; set; }

        [Category("Metro")]
        [Browsable(false)]
        public new TabSizeMode SizeMode { get; set; } = TabSizeMode.Fixed;

        [Category("Metro")]
        [Browsable(false)]
        public new TabDrawMode DrawMode { get; set; } = TabDrawMode.Normal;

        [Category("Metro"), Description("Gets or sets the backgorund color.")]
        public Color BackgroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets the foregorund color.")]
        private Color ForegroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets the tabpage text while un-selected.")]
        public Color UnselectedTextColor
        {
            get => _unselectedTextColor;
            set
            {
                _unselectedTextColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the tabpage text while selected.")]
        public Color SelectedTextColor
        {
            get => _selectedTextColor;
            set
            {
                _selectedTextColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the tancontrol apperance style.")]
        [DefaultValue(TabStyle.Style1)]
        public TabStyle TabStyle
        {
            get => _tabStyle;
            set
            {
                _tabStyle = value;
                if (_tabStyle == TabStyle.Style1)
                {
                    SelectedTextColor = Color.White;
                }
                else if (_tabStyle == TabStyle.Style2)
                {
                    SelectedTextColor = ForegroundColor;
                }
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
                     "Set it to false if you want the style of this control be independent. ")]
        public bool IsDerivedStyle
        {
            get => _isDerivedStyle;
            set
            {
                _isDerivedStyle = value;
                Refresh();
            }
        }

        #endregion Properties

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(BackgroundColor);
            int h = ItemSize.Height + 2;
            switch (TabStyle)
            {
                case TabStyle.Style1:
                    using (Pen sb = new(ForegroundColor, 2))
                    {
                        g.DrawLine(sb, 2, h, Width - 3, h);
                    }

                    for (int i = 0; i <= TabCount - 1; i++)
                    {
                        Rectangle r = GetTabRect(i);

                        if (i == SelectedIndex)
                        {
                            using SolidBrush sb = new(ForegroundColor);
                            g.FillRectangle(sb, r);
                        }

                        using SolidBrush tb = new(i == SelectedIndex ? SelectedTextColor : UnselectedTextColor);
                        g.DrawString(TabPages[i].Text, Font, tb, r, _mth.SetPosition());
                    }
                    break;
                case TabStyle.Style2:
                    for (int i = 0; i <= TabCount - 1; i++)
                    {
                        Rectangle r = GetTabRect(i);

                        if (i == SelectedIndex)
                        {
                            using Pen sb = new(ForegroundColor, 2);
                            g.DrawLine(sb, r.X, r.Height, r.X + r.Width, r.Height);
                        }

                        using SolidBrush tb = new(i == SelectedIndex ? SelectedTextColor : UnselectedTextColor);
                        g.DrawString(TabPages[i].Text, Font, tb, r, _mth.SetPosition());
                    }
                    break;
            }
        }

        #endregion Draw Control

        #region Events

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            for (int i = 0; i <= TabCount - 1; i++)
            {
                Rectangle r = GetTabRect(i);
                if (!r.Contains(e.Location))
                {
                    continue;
                }

                if (MCursor != Cursor)
                {
                    Cursor = MCursor;
                }

                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (MCursor == Cursor)
            {
                Cursor = Cursors.Default;
            }

            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            //_utl.SmoothCursor(ref m);
            _utl.SmoothCursor(ref m, Cursor);
            //_utl.NormalCursor(ref m, Cursor);

            base.WndProc(ref m);
        }

        #region Animation

        private int _oldIndex;

        private void DoSlideAnimate(System.Windows.Forms.TabPage control1, System.Windows.Forms.TabPage control2, bool moveback)
        {
            _utl.InitControlHandle(control1);
            _utl.InitControlHandle(control2);
            _slideGraphics = Graphics.FromHwnd(control2.Handle);
            _slideBitmap = new(control1.Width + control2.Width, control1.Height + control2.Height);

            if (moveback)
            {
                control2.DrawToBitmap(_slideBitmap, new Rectangle(0, 0, control2.Width, control2.Height));
                control1.DrawToBitmap(_slideBitmap, new Rectangle(control2.Width, 0, control1.Width, control1.Height));
            }
            else
            {
                control1.DrawToBitmap(_slideBitmap, new Rectangle(0, 0, control1.Width, control1.Height));
                control2.DrawToBitmap(_slideBitmap, new Rectangle(control1.Width, 0, control2.Width, control2.Height));
            }

            if (ControlsVisible)
            {
                foreach (Control c in control2.Controls)
                {
                    c.Hide();
                }
            }

            _slideAnimator.Update = (alpha) =>
            {
                _slideGraphics.DrawImage(_slideBitmap, alpha);
            };

            _slideAnimator.Complete = () =>
            {
                SelectedTab = control2;
                if (ControlsVisible)
                {
                    foreach (Control c in control2.Controls)
                    {
                        c.Show();
                    }
                }
            };

            _slideAnimator.Start
            (
                AnimateTime,
                new Point(moveback ? -control2.Width : 0, 0),
                new Point(moveback ? 0 : -control1.Width, 0),
                AnimateEasingType
            );
        }

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            if (!UseAnimation)
            {
                return;
            }

            if (_slideAnimator.Active)
            {
                e.Cancel = true;
                return;
            }

            DoSlideAnimate(TabPages[_oldIndex], TabPages[e.TabPageIndex], _oldIndex > e.TabPageIndex);
        }

        protected override void OnDeselecting(TabControlCancelEventArgs e)
        {
            _oldIndex = e.TabPageIndex;
        }

        private void DoAnimationScrollRight(Control control1, Control control2)
        {
            Graphics g = control1.CreateGraphics();

            Bitmap p1 = new(control1.Width, control1.Height);
            Bitmap p2 = new(control2.Width, control2.Height);

            control1.DrawToBitmap(p1, new Rectangle(0, 0, control1.Width, control1.Height));
            control2.DrawToBitmap(p2, new Rectangle(0, 0, control2.Width, control2.Height));

            if (ControlsVisible)
            {
                foreach (Control c in control1.Controls)
                {
                    c.Hide();
                }
            }

            int slide = control1.Width - (control1.Width % Speed);

            int a;

            for (a = 0; a >= -slide; a += -Speed)
            {
                g.DrawImage(p1, new Rectangle(a, 0, control1.Width, control1.Height));
                g.DrawImage(p2, new Rectangle(a + control2.Width, 0, control2.Width, control2.Height));
            }

            a = control1.Width;

            g.DrawImage(p1, new Rectangle(a, 0, control1.Width, control1.Height));
            g.DrawImage(p2, new Rectangle(a + control2.Width, 0, control2.Width, control2.Height));

            SelectedTab = (System.Windows.Forms.TabPage)control2;

            if (ControlsVisible)
            {
                foreach (Control c in control2.Controls)
                {
                    c.Show();
                }

                foreach (Control c in control1.Controls)
                {
                    c.Show();
                }
            }
        }

        #endregion Animation

        #endregion Events

        #region Methods

        private void InvalidateTabPage(Color c)
        {
            foreach (MetroTabPage T in TabPages)
            {
                T.Style = Style;
                T.BaseColor = c;
                T.Invalidate();
            }
        }

        #endregion
    }

    #endregion
}