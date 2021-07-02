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

        private PanelSide _Side;
        [Browsable(true)]
        [Description("Determines the foreground color of the label according to which side it is placed on.")]
        public PanelSide Side
        {
            get => _Side;
            set
            {
                _Side = value;
                switch (value)
                {
                    case PanelSide.LeftPanel:
                        ForeColor = _LeftSideForeColor;
                        break;
                    case PanelSide.RightPanel:
                        ForeColor = _RightSideForeColor;
                        break;
                }
                Invalidate();
            }
        }

        private Color _LeftSideForeColor = ColorTranslator.FromHtml("#FAFAFA");
        public Color LeftSideForeColor
        {
            get => _LeftSideForeColor;
            set
            {
                _LeftSideForeColor = value;
                Invalidate();
            }
        }

        private Color _RightSideForeColor = ColorTranslator.FromHtml("#AAABB0");
        public Color RightSideForeColor
        {
            get => _RightSideForeColor;
            set
            {
                _RightSideForeColor = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        [Browsable(true)]
        [Description("Specifies the quality of text rendering.")]
        public TextRenderingHint TextRenderingHint
        {
            get => _TextRenderingHint;
            set
            {
                _TextRenderingHint = value;
                Invalidate();
            }
        }

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
            ForeColor = _RightSideForeColor;
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
            e.Graphics.TextRenderingHint = _TextRenderingHint;
            base.OnPaint(e);
        }
    }

    #endregion
}