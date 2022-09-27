#region Imports

using ReaLTaiizor.Child.Poison;
using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Extension.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
using ReaLTaiizor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonTabControl

#if NET48_OR_GREATER
    [Designer(typeof(PoisonTabControlDesigner))]
#endif
    [ToolboxBitmap(typeof(TabControl))]
    public class PoisonTabControl : TabControl, IPoisonControl
    {
        #region Interface
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                {
                    return poisonStyle;
                }

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                {
                    return StyleManager.Style;
                }

                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                {
                    return PoisonDefaults.Style;
                }

                return poisonStyle;
            }
            set => poisonStyle = value;
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                {
                    return poisonTheme;
                }

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }

                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                {
                    return PoisonDefaults.Theme;
                }

                return poisonTheme;
            }
            set => poisonTheme = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager { get; set; } = null;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor { get; set; } = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor { get; set; } = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors { get; set; } = false;

        [Browsable(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }

        #endregion

        #region Fields
        //Additional variables to be used by HideTab and ShowTab
        private readonly List<string> tabDisable = new();
        private readonly List<string> tabOrder = new();
        private readonly List<HiddenTabs> hidTabs = new();

        private SubClass scUpDown = null;
        private bool bUpDown = false;

        private const int TabBottomBorderHeight = 3;

        [DefaultValue(PoisonTabControlSize.Medium)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonTabControlSize FontSize { get; set; } = PoisonTabControlSize.Medium;
        [DefaultValue(PoisonTabControlWeight.Light)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonTabControlWeight FontWeight { get; set; } = PoisonTabControlWeight.Light;
        [DefaultValue(ContentAlignment.MiddleLeft)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleLeft;

        [Editor(typeof(PoisonTabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages => base.TabPages;


        private bool isMirrored;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public new bool IsMirrored
        {
            get => isMirrored;
            set
            {
                if (isMirrored == value)
                {
                    return;
                }

                isMirrored = value;
                UpdateStyles();
            }
        }

        #endregion

        #region Constructor

        public PoisonTabControl()
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

            Padding = new(6, 8);
            Selecting += PoisonTabControl_Selecting;
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
                }

                if (backColor.A == 255 && BackgroundImage == null)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new PoisonPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            for (int index = 0; index < TabPages.Count; index++)
            {
                if (index != SelectedIndex)
                {
                    DrawTab(index, e.Graphics);
                }
            }
            if (SelectedIndex <= -1)
            {
                return;
            }

            DrawTabBottomBorder(SelectedIndex, e.Graphics);
            DrawTab(SelectedIndex, e.Graphics);
            DrawTabSelected(SelectedIndex, e.Graphics);

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
        }

        private void DrawTabBottomBorder(int index, Graphics graphics)
        {
            using Brush bgBrush = new SolidBrush(PoisonPaint.BorderColor.TabControl.Normal(Theme));
            Rectangle borderRectangle = new(DisplayRectangle.X, GetTabRect(index).Bottom + 2 - TabBottomBorderHeight, DisplayRectangle.Width, TabBottomBorderHeight);
            graphics.FillRectangle(bgBrush, borderRectangle);
        }

        private void DrawTabSelected(int index, Graphics graphics)
        {
            using Brush selectionBrush = new SolidBrush(PoisonPaint.GetStyleColor(Style));
            Rectangle selectedTabRect = GetTabRect(index);
            Rectangle borderRectangle = new(selectedTabRect.X + ((index == 0) ? 2 : 0), GetTabRect(index).Bottom + 2 - TabBottomBorderHeight, selectedTabRect.Width + ((index == 0) ? 0 : 2), TabBottomBorderHeight);
            graphics.FillRectangle(selectionBrush, borderRectangle);
        }

        private Size MeasureText(string text)
        {
            Size preferredSize;
            using (Graphics g = CreateGraphics())
            {
                Size proposedSize = new(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, text, PoisonFonts.TabControl(FontSize, FontWeight), proposedSize, PoisonPaint.GetTextFormatFlags(TextAlign) | TextFormatFlags.NoPadding);
            }
            return preferredSize;
        }

        private void DrawTab(int index, Graphics graphics)
        {
            Color foreColor;
            Color backColor = BackColor;

            if (!UseCustomBackColor)
            {
                backColor = PoisonPaint.BackColor.Form(Theme);
            }

            System.Windows.Forms.TabPage tabPage = TabPages[index];
            Rectangle tabRect = GetTabRect(index);

            if (!Enabled || tabDisable.Contains(tabPage.Name))
            {
                foreColor = PoisonPaint.ForeColor.Label.Disabled(Theme);
            }
            else
            {
                if (UseCustomForeColor)
                {
                    foreColor = DefaultForeColor;
                }
                else
                {
                    foreColor = !UseStyleColors ? PoisonPaint.ForeColor.TabControl.Normal(Theme) : PoisonPaint.GetStyleColor(Style);
                }
            }

            if (index == 0)
            {
                tabRect.X = DisplayRectangle.X;
            }

            Rectangle bgRect = tabRect;

            tabRect.Width += 20;

            using (Brush bgBrush = new SolidBrush(backColor))
            {
                graphics.FillRectangle(bgBrush, bgRect);
            }

            TextRenderer.DrawText(graphics, tabPage.Text, PoisonFonts.TabControl(FontSize, FontWeight), tabRect, foreColor, backColor, PoisonPaint.GetTextFormatFlags(TextAlign));
        }

        [SecuritySafeCritical]
        private void DrawUpDown(Graphics graphics)
        {
            Color backColor = Parent != null ? Parent.BackColor : PoisonPaint.BackColor.Form(Theme);

            Rectangle borderRect = new();
            _ = WinApi.GetClientRect(scUpDown.Handle, ref borderRect);

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            graphics.Clear(backColor);

            using Brush b = new SolidBrush(PoisonPaint.BorderColor.TabControl.Normal(Theme));
            GraphicsPath gp = new(FillMode.Winding);
            PointF[] pts = { new PointF(6, 6), new PointF(16, 0), new PointF(16, 12) };
            gp.AddLines(pts);

            graphics.FillPath(b, gp);

            gp.Reset();

            PointF[] pts2 = { new PointF(borderRect.Width - 15, 0), new PointF(borderRect.Width - 5, 6), new PointF(borderRect.Width - 15, 12) };
            gp.AddLines(pts2);

            graphics.FillPath(b, gp);

            gp.Dispose();
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        [SecuritySafeCritical]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!DesignMode)
            {
                WinApi.ShowScrollBar(Handle, (int)WinApi.ScrollBar.SB_BOTH, 0);
            }
        }

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int WS_EX_LAYOUTRTL = 0x400000;
                const int WS_EX_NOINHERITLAYOUT = 0x100000;
                CreateParams cp = base.CreateParams;
                if (isMirrored)
                {
                    cp.ExStyle = cp.ExStyle | WS_EX_LAYOUTRTL | WS_EX_NOINHERITLAYOUT;
                }

                return cp;
            }
        }

        private new Rectangle GetTabRect(int index)
        {
            if (index < 0)
            {
                return new Rectangle();
            }

            Rectangle baseRect = base.GetTabRect(index);
            return baseRect;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (!TabPages[SelectedIndex].Focused)
                {
                    bool subControlFocused = false;
                    foreach (Control ctrl in TabPages[SelectedIndex].Controls)
                    {
                        if (ctrl.Focused)
                        {
                            subControlFocused = true;
                            return;
                        }
                    }

                    if (!subControlFocused)
                    {
                        TabPages[SelectedIndex].Select();
                        TabPages[SelectedIndex].Focus();
                    }
                }
            }

            base.OnMouseWheel(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            OnFontChanged(EventArgs.Empty);
            FindUpDown();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            FindUpDown();
            UpdateUpDown();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            FindUpDown();
            UpdateUpDown();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            UpdateUpDown();
            Invalidate();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1d;

        [SecuritySafeCritical]
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr hFont = PoisonFonts.TabControl(FontSize, FontWeight).ToHfont();
            SendMessage(Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            UpdateStyles();
        }

        private void PoisonTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabDisable.Count > 0 && tabDisable.Contains(e.TabPage.Name))
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Helper Methods

        [SecuritySafeCritical]
        private void FindUpDown()
        {
            if (!DesignMode)
            {
                bool bFound = false;

                IntPtr pWnd = WinApi.GetWindow(Handle, WinApi.GW_CHILD);

                while (pWnd != IntPtr.Zero)
                {
                    char[] className = new char[33];

                    int length = WinApi.GetClassName(pWnd, className, 32);

                    string s = new(className, 0, length);

                    if (s == "msctls_updown32")
                    {
                        bFound = true;

                        if (!bUpDown)
                        {
                            scUpDown = new SubClass(pWnd, true);
                            scUpDown.SubClassedWndProc += new SubClass.SubClassWndProcEventHandler(scUpDown_SubClassedWndProc);

                            bUpDown = true;
                        }
                        break;
                    }

                    pWnd = WinApi.GetWindow(pWnd, WinApi.GW_HWNDNEXT);
                }

                if ((!bFound) && bUpDown)
                {
                    bUpDown = false;
                }
            }
        }

        [SecuritySafeCritical]
        private void UpdateUpDown()
        {
            if (bUpDown && !DesignMode)
            {
                if (WinApi.IsWindowVisible(scUpDown.Handle))
                {
                    Rectangle rect = new();
                    _ = WinApi.GetClientRect(scUpDown.Handle, ref rect);
                    _ = WinApi.InvalidateRect(scUpDown.Handle, ref rect, true);
                }
            }
        }

        [SecuritySafeCritical]
        private int scUpDown_SubClassedWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WinApi.Messages.WM_PAINT:
                    IntPtr hDC = WinApi.GetWindowDC(scUpDown.Handle);
                    Graphics g = Graphics.FromHdc(hDC);
                    DrawUpDown(g);
                    g.Dispose();
                    _ = WinApi.ReleaseDC(scUpDown.Handle, hDC);
                    m.Result = IntPtr.Zero;
                    Rectangle rect = new();
                    _ = WinApi.GetClientRect(scUpDown.Handle, ref rect);
                    _ = WinApi.ValidateRect(scUpDown.Handle, ref rect);
                    return 1;
            }
            return 0;
        }

        #endregion

        #region Additional Functions
        public void HideTab(PoisonTabPage tabpage)
        {
            if (TabPages.Contains(tabpage))
            {
                int _tabid = TabPages.IndexOf(tabpage);

                hidTabs.Add(new HiddenTabs(_tabid, tabpage.Name));
                TabPages.Remove(tabpage);
            }
        }

        public void ShowTab(PoisonTabPage tabpage)
        {
            HiddenTabs result = hidTabs.Find
            (
                 delegate (HiddenTabs bk)
                 {
                     return bk.Tabpage == tabpage.Name;
                 }
             );

            if (result != null)
            {
                TabPages.Insert(result.Index, tabpage);
                hidTabs.Remove(result);
            }
        }

        public void DisableTab(PoisonTabPage tabpage)
        {
            if (!tabDisable.Contains(tabpage.Name))
            {
                if (SelectedTab == tabpage && TabCount == 1)
                {
                    return;
                }

                if (SelectedTab == tabpage)
                {
                    if (SelectedIndex == TabCount - 1)
                    { SelectedIndex = 0; }
                    else { SelectedIndex++; }
                }

                int _tabid = TabPages.IndexOf(tabpage);

                tabDisable.Add(tabpage.Name);
                Graphics e = CreateGraphics();
                DrawTab(_tabid, e);
                DrawTabBottomBorder(SelectedIndex, e);
                DrawTabSelected(SelectedIndex, e);
            }
        }

        public void EnableTab(PoisonTabPage tabpage)
        {
            if (tabDisable.Contains(tabpage.Name))
            {
                tabDisable.Remove(tabpage.Name);
                int _tabid = TabPages.IndexOf(tabpage);

                Graphics e = CreateGraphics();
                DrawTab(_tabid, e);
                DrawTabBottomBorder(SelectedIndex, e);
                DrawTabSelected(SelectedIndex, e);
            }
        }

        public bool IsTabEnable(PoisonTabPage tabpage)
        {
            return tabDisable.Contains(tabpage.Name);
        }

        public bool IsTabHidden(PoisonTabPage tabpage)
        {
            HiddenTabs result = hidTabs.Find
            (
                delegate (HiddenTabs bk)
                {
                    return bk.Tabpage == tabpage.Name;
                }
            );

            return result != null;
        }
        #endregion
    }

    #endregion
}