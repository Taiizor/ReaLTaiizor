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
            base.Renderer = new KitMenuStripRenderer(base.BackColor, backColor, selectedBackColor, hoverBackColor, textColor, hoverTextColor, selectedTextColor, seperatorColor);
            base.BackColor = Color.DodgerBlue;
        }

        private void RefreshUI()
        {
            base.Renderer = new KitMenuStripRenderer(base.BackColor, backColor, selectedBackColor, hoverBackColor, textColor, hoverTextColor, selectedTextColor, seperatorColor);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Item background color")]
        public Color ItemBackColor
        {
            get => backColor;
            set
            {
                backColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Selected item background color")]
        public Color SelectedBackColor
        {
            get => selectedBackColor;
            set
            {
                selectedBackColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hover item background color")]
        public Color HoverBackColor
        {
            get => hoverBackColor;
            set
            {
                hoverBackColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Item text color")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hover item text color")]
        public Color HoverTextColor
        {
            get => hoverTextColor;
            set
            {
                hoverTextColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Selected item text color")]
        public Color SelectedTextColor
        {
            get => selectedTextColor;
            set
            {
                selectedTextColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Seperator color")]
        public Color SeperatorColor
        {
            get => seperatorColor;
            set
            {
                seperatorColor = value;
                RefreshUI();
            }
        }

        private Color backColor = Color.DodgerBlue;

        private Color selectedBackColor = Color.DarkOrchid;

        private Color hoverBackColor = Color.RoyalBlue;

        private Color textColor = Color.White;

        private Color hoverTextColor = Color.White;

        private Color selectedTextColor = Color.White;

        private Color seperatorColor = Color.White;
    }

    #endregion
}