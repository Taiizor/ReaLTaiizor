#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalMenuStrip

    public class RoyalMenuStrip : MenuStrip
    {
        private Color hotTrackColor;
        public Color HotTrackColor
        {
            get => hotTrackColor;
            set { hotTrackColor = value; Invalidate(); }
        }

        private Color selectedColor;
        public Color SelectedColor
        {
            get => selectedColor;
            set { selectedColor = value; Invalidate(); }
        }

        public RoyalMenuStrip()
        {
            Renderer = new RoyalToolStripRenderer();
            Dock = DockStyle.None;
            AutoSize = false;
            Padding = new Padding(1);
            Size = new(100, 30);
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            int width = 1;

            for (int i = 0; i < Items.Count; i++)
            {
                width += Items[i].Width;
            }

            width += 1;

            Size = new(width, 30);

            base.OnItemAdded(e);
        }

        protected override void OnItemRemoved(ToolStripItemEventArgs e)
        {
            int width = 1;

            for (int i = 0; i < Items.Count; i++)
            {
                width += Items[i].Width;
            }

            width += 1;

            Size = new(width, 30);

            base.OnItemRemoved(e);
        }
    }

    #endregion
}