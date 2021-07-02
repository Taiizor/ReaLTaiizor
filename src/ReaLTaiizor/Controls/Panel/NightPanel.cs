#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightPanel

    public class NightPanel : System.Windows.Forms.Panel
    {
        #region Enum

        public enum PanelSide
        {
            Left,
            Right
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [Description("The background color of the component.")]
        public override Color BackColor { get; set; }

        private PanelSide _Side = PanelSide.Left;
        public PanelSide Side
        {
            get => _Side;
            set
            {
                _Side = value;
                if (_Side == PanelSide.Left)
                {
                    BackColor = LeftSideColor;
                }
                else
                {
                    BackColor = RightSideColor;
                }

                Invalidate();
            }
        }

        private Color _LeftSideColor = ColorTranslator.FromHtml("#F25D59");
        public Color LeftSideColor
        {
            get => _LeftSideColor;
            set
            {
                _LeftSideColor = value;
                Invalidate();
            }
        }

        private Color _RightSideColor = ColorTranslator.FromHtml("#292C3D");
        public Color RightSideColor
        {
            get => _RightSideColor;
            set
            {
                _RightSideColor = value;
                Invalidate();
            }
        }

        #endregion

        protected override void OnClick(EventArgs e)
        {
            Focus();
            base.OnClick(e);
        }

        public NightPanel()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            UpdateStyles();

            ForeColor = ColorTranslator.FromHtml("#FAFAFA");
            BackColor = RightSideColor;

            BorderStyle = BorderStyle.None;
        }
    }

    #endregion
}