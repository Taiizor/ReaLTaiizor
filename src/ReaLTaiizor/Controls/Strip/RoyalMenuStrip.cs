#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalMenuStrip

    public class RoyalMenuStrip : MenuStrip
    {
        Color hotTrackColor;
        public Color HotTrackColor
        {
            get { return hotTrackColor; }
            set { hotTrackColor = value; Invalidate(); }
        }

        Color selectedColor;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; Invalidate(); }
        }

        public RoyalMenuStrip()
        {
            Renderer = new RoyalToolStripRenderer();
            Dock = DockStyle.None;
            AutoSize = false;
            Padding = new Padding(1);
            Size = new Size(100, 30);
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            int width = 1;

            for (int i = 0; i < Items.Count; i++)
                width += Items[i].Width;

            width += 1;

            Size = new Size(width, 30);

            base.OnItemAdded(e);
        }

        protected override void OnItemRemoved(ToolStripItemEventArgs e)
        {
            int width = 1;

            for (int i = 0; i < Items.Count; i++)
                width += Items[i].Width;

            width += 1;

            Size = new Size(width, 30);

            base.OnItemRemoved(e);
        }
    }

    #endregion
}