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

        public PanelSide Side
        {
            get;
            set
            {
                field = value;
                if (field == PanelSide.Left)
                {
                    BackColor = LeftSideColor;
                }
                else
                {
                    BackColor = RightSideColor;
                }

                Invalidate();
            }
        } = PanelSide.Left;

        public Color LeftSideColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#F25D59");

        public Color RightSideColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#292C3D");

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