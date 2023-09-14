#region Imports

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotPieGraph

    public class ParrotPieGraph : Control
    {
        public ParrotPieGraph()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(40, 40, 40);
            base.Size = new Size(100, 100);
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private List<Color> _Colors = new()
        {
            Color.FromArgb(249, 55, 98),
            Color.FromArgb(219, 55, 128),
            Color.FromArgb(193, 58, 151),
            Color.FromArgb(166, 58, 182),
            Color.FromArgb(147, 61, 180),
            Color.FromArgb(126, 66, 186),
            Color.FromArgb(107, 70, 188),
            Color.FromArgb(77, 94, 210),
            Color.FromArgb(48, 119, 227),
            Color.FromArgb(23, 144, 249),
            Color.FromArgb(10, 148, 249),
            Color.FromArgb(0, 152, 250),
            Color.FromArgb(0, 162, 250),
            Color.FromArgb(0, 150, 212)
        };
        public List<Color> Colors
        {
            get => _Colors;
            set
            {
                _Colors = value;
                Invalidate();
            }
        }

        private List<int> _Numbers = new()
        {
            5,
            10,
            6,
            4,
            9,
            11,
            3,
            15,
            12,
            17,
            3,
            4,
            6
        };
        public List<int> Numbers
        {
            get => _Numbers;
            set
            {
                int num = 0;
                foreach (int num2 in value)
                {
                    num += num2;
                }
                if (num == 100)
                {
                    _Numbers = value;
                }
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            foreach (int num4 in Numbers)
            {
                num3++;
                if (num3 == Numbers.Count)
                {
                    e.Graphics.FillPie(new SolidBrush(Colors[num2]), 0, 0, base.Width, base.Height, num, 360 - num);
                }
                else
                {
                    e.Graphics.FillPie(new SolidBrush(Colors[num2]), 0, 0, base.Width, base.Height, num, (int)(num4 * 3.6));
                }
                num2++;
                if (num2 == Numbers.Count + 1)
                {
                    Colors.Reverse();
                    num2 = 0;
                }
                num += (int)(num4 * 3.6);
            }
            base.OnPaint(e);
        }
    }

    #endregion
}