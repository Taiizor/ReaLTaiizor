#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Helper;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Interface.Poison;

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
                CustomPaintBackground(this, e);
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
                CustomPaint(this, e);
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
                CustomPaintForeground(this, e);
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category("Poison Appearance")]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                    return poisonStyle;

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                    return StyleManager.Style;
                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                    return ColorStyle.Blue;

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
                    return poisonTheme;

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                    return StyleManager.Theme;
                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                    return ThemeStyle.Light;

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

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomBackColor
        {
            get => useCustomBackColor;
            set => useCustomBackColor = value;
        }

        private bool useCustomForeColor = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomForeColor
        {
            get => useCustomForeColor;
            set => useCustomForeColor = value;
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseStyleColors
        {
            get => useStyleColors;
            set => useStyleColors = value;
        }

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
        private float _offset = 0.2F;
        [DefaultValue(0.2F)]
        public float HighLightPercentage { get => _offset; set => _offset = value; }
        #endregion

        private PoisonDataGridHelper scrollhelper = null;
        private PoisonDataGridHelper scrollhelperH = null;

        public PoisonDataGridView()
        {
            InitializeComponent();

            StyleGrid();

            this.Controls.Add(_vertical);
            this.Controls.Add(_horizontal);

            this.Controls.SetChildIndex(_vertical, 0);
            this.Controls.SetChildIndex(_horizontal, 1);

            _horizontal.Visible = false;
            _vertical.Visible = false;

            scrollhelper = new PoisonDataGridHelper(_vertical, this);
            scrollhelperH = new PoisonDataGridHelper(_horizontal, this, false);

            this.DoubleBuffered = true;
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
            if (this.RowCount > 1)
            {
                if (e.Delta > 0 && this.FirstDisplayedScrollingRowIndex > 0)
                    this.FirstDisplayedScrollingRowIndex--;
                else if (e.Delta < 0)
                    this.FirstDisplayedScrollingRowIndex++;
            }
        }

        private void StyleGrid()
        {
            this.BorderStyle = BorderStyle.None;
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.EnableHeadersVisualStyles = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.BackColor = PoisonPaint.BackColor.Form(Theme);
            this.BackgroundColor = PoisonPaint.BackColor.Form(Theme);
            this.GridColor = PoisonPaint.BackColor.Form(Theme);
            this.ForeColor = PoisonPaint.ForeColor.Button.Disabled(Theme);
            this.Font = new Font("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);

            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AllowUserToResizeRows = false;

            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.ColumnHeadersDefaultCellStyle.BackColor = PoisonPaint.GetStyleColor(Style);
            this.ColumnHeadersDefaultCellStyle.ForeColor = PoisonPaint.ForeColor.Button.Press(Theme);

            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.RowHeadersDefaultCellStyle.BackColor = PoisonPaint.GetStyleColor(Style);
            this.RowHeadersDefaultCellStyle.ForeColor = PoisonPaint.ForeColor.Button.Press(Theme);

            this.DefaultCellStyle.BackColor = PoisonPaint.BackColor.Form(Theme);

            this.DefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), _offset);
            this.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);

            this.DefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), _offset);
            this.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);

            this.RowHeadersDefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), _offset);
            this.RowHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);

            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = ControlPaint.Light(PoisonPaint.GetStyleColor(Style), _offset);
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);
        }
    }

    #endregion
}