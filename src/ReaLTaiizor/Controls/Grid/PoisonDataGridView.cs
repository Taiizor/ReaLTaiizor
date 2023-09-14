#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Helper;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonDataGridView

    public partial class PoisonDataGridView : DataGridView, IPoisonControl
    {
        #region Interface
        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category("Poison Appearance")]
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
                    return ColorStyle.Blue;
                }

                return poisonStyle;
            }
            set { poisonStyle = value; StyleGrid(); }
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category("Poison Appearance")]
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
                    return ThemeStyle.Light;
                }

                return poisonTheme;
            }
            set { poisonTheme = value; StyleGrid(); }
        }

        private PoisonStyleManager poisonStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager
        {
            get => poisonStyleManager;
            set { poisonStyleManager = value; StyleGrid(); }
        }

        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomBackColor { get; set; } = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomForeColor { get; set; } = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseStyleColors { get; set; } = false;

        [Browsable(false)]
        [Category("Poison Behaviour")]
        [DefaultValue(true)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }
        #endregion

        #region Properties
        [DefaultValue(0.2F)]
        public float HighLightPercentage { get; set; } = 0.2F;
        #endregion

        private readonly PoisonDataGridHelper scrollhelper = null;
        private readonly PoisonDataGridHelper scrollhelperH = null;

        public PoisonDataGridView()
        {
            InitializeComponent();

            StyleGrid();

            Controls.Add(_vertical);
            Controls.Add(_horizontal);

            Controls.SetChildIndex(_vertical, 0);
            Controls.SetChildIndex(_horizontal, 1);

            _horizontal.Visible = false;
            _vertical.Visible = false;

            scrollhelper = new PoisonDataGridHelper(_vertical, this);
            scrollhelperH = new PoisonDataGridHelper(_horizontal, this, false);

            DoubleBuffered = true;
        }

        protected override void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            base.OnColumnStateChanged(e);
            if (e.StateChanged == DataGridViewElementStates.Visible)
            {
                scrollhelper.UpdateScrollbar();
                scrollhelperH.UpdateScrollbar();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (RowCount > 1)
            {
                if (e.Delta > 0 && FirstDisplayedScrollingRowIndex > 0)
                {
                    FirstDisplayedScrollingRowIndex--;
                }
                else if (e.Delta < 0)
                {
                    FirstDisplayedScrollingRowIndex++;
                }
            }
        }

        private void StyleGrid()
        {
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            EnableHeadersVisualStyles = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BackColor = PoisonPaint.BackColor.Form(Theme);
            BackgroundColor = PoisonPaint.BackColor.Form(Theme);
            GridColor = PoisonPaint.BackColor.Form(Theme);
            ForeColor = PoisonPaint.ForeColor.Button.Disabled(Theme);
            Font = new("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);

            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            AllowUserToResizeRows = false;

            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ColumnHeadersDefaultCellStyle.BackColor = PoisonPaint.GetStyleColor(Style);
            ColumnHeadersDefaultCellStyle.ForeColor = PoisonPaint.ForeColor.Button.Press(Theme);

            RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            RowHeadersDefaultCellStyle.BackColor = PoisonPaint.GetStyleColor(Style);
            RowHeadersDefaultCellStyle.ForeColor = PoisonPaint.ForeColor.Button.Press(Theme);

            DefaultCellStyle.BackColor = PoisonPaint.BackColor.Form(Theme);

            DefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), HighLightPercentage);
            DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);

            DefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), HighLightPercentage);
            DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);

            RowHeadersDefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), HighLightPercentage);
            RowHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);

            ColumnHeadersDefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), HighLightPercentage);
            ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);
        }
    }

    #endregion
}