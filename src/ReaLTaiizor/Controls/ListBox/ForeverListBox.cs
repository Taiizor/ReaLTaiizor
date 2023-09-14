#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverListBox

    public class ForeverListBox : Control
    {
        private ListBox withEventsField_ListBx = new();
        private ListBox ListBx
        {
            get => withEventsField_ListBx;
            set
            {
                if (withEventsField_ListBx != null)
                {
                    withEventsField_ListBx.DrawItem -= Drawitem;
                }

                withEventsField_ListBx = value;
                if (withEventsField_ListBx != null)
                {
                    withEventsField_ListBx.DrawItem += Drawitem;
                }
            }
        }

        private string[] _items = { "" };

        [Category("Options")]
        public string[] items
        {
            get => _items;
            set
            {
                _items = value;
                ListBx.Items.Clear();
                ListBx.Items.AddRange(value);
                Invalidate();
            }
        }

        [Category("Colors")]
        public Color SelectedColor { get; set; } = ForeverLibrary.ForeverColor;

        public string SelectedItem =>
                //return ListBx.SelectedItem.ToString();
                (string)ListBx.Items[SelectedIndex];

        public int SelectedIndex
        {
            get
            {
                int functionReturnValue = 0;
                //return ListBx.SelectedIndex;
                if (ListBx.SelectedIndex < 0)
                {
                    return functionReturnValue;
                }

                return functionReturnValue;
            }
        }

        public void Clear()
        {
            ListBx.Items.Clear();
        }

        public void ClearSelected()
        {
            for (int i = ListBx.SelectedItems.Count - 1; i >= 0; i += -1)
            {
                ListBx.Items.Remove(ListBx.SelectedItems[i]);
            }
        }

        public void Drawitem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();
            e.DrawFocusRectangle();

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            //-- if selected
            if (e.State.ToString().IndexOf("Selected,") >= 0)
            {
                //-- Base
                e.Graphics.FillRectangle(new SolidBrush(SelectedColor), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

                //-- Text
                e.Graphics.DrawString(" " + ListBx.Items[e.Index].ToString(), new Font("Segoe UI", 8), Brushes.White, e.Bounds.X, e.Bounds.Y + 2);
            }
            else
            {
                //-- Base
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 53, 55)), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));

                //-- Text 
                e.Graphics.DrawString(" " + ListBx.Items[e.Index].ToString(), new Font("Segoe UI", 8), Brushes.White, e.Bounds.X, e.Bounds.Y + 2);
            }

            e.Graphics.Dispose();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(ListBx))
            {
                Controls.Add(ListBx);
            }
        }

        public void AddRange(object[] items)
        {
            ListBx.Items.Remove("");
            ListBx.Items.AddRange(items);
        }

        public void AddItem(object item)
        {
            ListBx.Items.Remove("");
            ListBx.Items.Add(item);
        }

        private readonly Color BaseColor = Color.FromArgb(45, 47, 49);

        public ForeverListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            ListBx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            ListBx.ScrollAlwaysVisible = false;
            ListBx.HorizontalScrollbar = false;
            ListBx.BorderStyle = BorderStyle.None;
            ListBx.BackColor = BaseColor;
            ListBx.ForeColor = Color.White;
            ListBx.Location = new(3, 3);
            ListBx.Font = new("Segoe UI", 8);
            ListBx.ItemHeight = 20;
            ListBx.Items.Clear();
            ListBx.IntegralHeight = false;

            Size = new(131, 101);
            BackColor = BaseColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Rectangle Base = new(0, 0, Width, Height);

            Graphics _with19 = G;
            _with19.SmoothingMode = SmoothingMode.HighQuality;
            _with19.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with19.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with19.Clear(BackColor);

            //-- Size
            ListBx.Size = new(Width - 6, Height - 2);

            //-- Base
            _with19.FillRectangle(new SolidBrush(BaseColor), Base);

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            SelectedColor = Colors.Forever;
        }
    }

    #endregion
}