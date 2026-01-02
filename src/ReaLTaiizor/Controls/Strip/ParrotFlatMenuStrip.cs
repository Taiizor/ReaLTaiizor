#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotFlatMenuStrip

    public class ParrotFlatMenuStrip : MenuStrip
    {
        public ParrotFlatMenuStrip()
        {
            base.Renderer = new KitMenuStripRenderer(base.BackColor, ItemBackColor, SelectedBackColor, HoverBackColor, TextColor, HoverTextColor, SelectedTextColor, SeparatorColor);
            base.BackColor = Color.DodgerBlue;
        }

        private void RefreshUI()
        {
            base.Renderer = new KitMenuStripRenderer(base.BackColor, ItemBackColor, SelectedBackColor, HoverBackColor, TextColor, HoverTextColor, SelectedTextColor, SeparatorColor);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Item background color")]
        public Color ItemBackColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.DodgerBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Selected item background color")]
        public Color SelectedBackColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.DarkOrchid;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hover item background color")]
        public Color HoverBackColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.RoyalBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Item text color")]
        public Color TextColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hover item text color")]
        public Color HoverTextColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Selected item text color")]
        public Color SelectedTextColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Separator color")]
        public Color SeparatorColor
        {
            get;
            set
            {
                field = value;
                RefreshUI();
            }
        } = Color.White;
    }

    #endregion
}