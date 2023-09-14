#region Imports

using ReaLTaiizor.Manager;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialCheckListBox

    public class MaterialCheckListBox : System.Windows.Forms.Panel, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public bool Striped { get; set; }

        public Color StripeDarkColor { get; set; }

        public ItemsList Items { get; set; }

        public MaterialCheckListBox() : base()
        {
            this.DoubleBuffered = true;
            this.Items = new ItemsList(this);
            this.AutoScroll = true;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    MessageBox.Show("Start!");

        //    foreach (Control CTRL in FindForm().Controls)
        //    {
        //        try
        //        {
        //            if (CTRL is MaterialCheckListBox)
        //            {
        //                MessageBox.Show("OK - " + CTRL.Name);
        //                MaterialCheckListBox MCLB = CTRL as MaterialCheckListBox;
        //                MessageBox.Show("YES - " + MCLB.Name + " - " + MCLB.Items.Count);
        //                foreach (var Item in MCLB.Items)
        //                {
        //                    if (!MCLB.Controls.Contains(Item))
        //                    {
        //                        MessageBox.Show("I - " + Item.Text);
        //                        Items.Add(Item.Text, Item.Checked);
        //                    }
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            //
        //        }
        //    }
        //}

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode)
            {
                BackColorChanged += (sender, args) => BackColor = Parent.BackColor;
                BackColor = Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor;
            }
            else
            {
                BackColorChanged += (sender, args) => BackColor = BlendColor(Parent.BackColor, SkinManager.BackgroundAlternativeColor, SkinManager.BackgroundAlternativeColor.A);
                BackColor = BlendColor(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor, SkinManager.BackgroundAlternativeColor, SkinManager.BackgroundAlternativeColor.A);
            }
        }

        public CheckState GetItemCheckState(int Index)
        {
            return Items[Index].CheckState;
        }

        public class ItemsList : List<MaterialCheckBox>
        {
            private System.Windows.Forms.Panel _parent;

            public ItemsList(System.Windows.Forms.Panel parent)
            {
                _parent = parent;
            }

            public delegate void SelectedIndexChangedEventHandler(int Index);

            public void Add(string text)
            {
                Add(text, false);
            }

            public void Add(string text, bool defaultValue)
            {
                MaterialCheckBox cb = new();
                Add(cb);
                cb.Checked = defaultValue;
                cb.Text = text;
            }

            public new void Add(MaterialCheckBox value)
            {
                base.Add(value);
                _parent.Controls.Add(value);
                value.Dock = DockStyle.Top;
            }

            public new void Remove(MaterialCheckBox value)
            {
                base.Remove(value);
                _parent.Controls.Remove(value);
            }
        }
    }

    #endregion
}