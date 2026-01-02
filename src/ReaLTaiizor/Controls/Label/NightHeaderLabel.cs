#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightHeaderLabel

    public class NightHeaderLabel : Label
    {
        #region Properties

        [Browsable(true)]
        [Description("Determines the foreground color of the label according to which side it is placed on.")]
        public PanelSide Side
        {
            get;
            set
            {
                field = value;
                switch (value)
                {
                    case PanelSide.LeftPanel:
                        ForeColor = LeftSideForeColor;
                        break;
                    case PanelSide.RightPanel:
                        ForeColor = RightSideForeColor;
                        break;
                }
                Invalidate();
            }
        }

        public Color LeftSideForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#FAFAFA");

        public Color RightSideForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#AAABB0");

        [Browsable(true)]
        [Description("Specifies the quality of text rendering.")]
        public TextRenderingHint TextRenderingHint
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.AntiAliasGridFit;

        #endregion

        #region Enum

        public enum PanelSide
        {
            LeftPanel,
            RightPanel
        };

        #endregion

        public NightHeaderLabel()
        {
            Font = new("Microsoft Sans Serif", 22, FontStyle.Regular, GraphicsUnit.Point);
            TextAlign = ContentAlignment.MiddleCenter;
            ForeColor = RightSideForeColor;
            BackColor = Color.Transparent;
            UseCompatibleTextRendering = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint;
            base.OnPaint(e);
        }
    }

    #endregion
}