#region Imports

using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialRichTextBox

    public class MaterialRichTextBox : System.Windows.Forms.RichTextBox, MaterialControlI
    {
        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        private const char EmptyChar = (char)0;

        private const char VisualStylePasswordChar = '\u25CF';

        private const char NonVisualStylePasswordChar = '\u002A';

        private string hint = string.Empty;

        public string Hint
        {
            get => hint;
            set
            {
                hint = value;
                SendMessage(Handle, EM_SETCUEBANNER, (int)IntPtr.Zero, Hint);
            }
        }

        public new void SelectAll()
        {
            BeginInvoke((MethodInvoker)delegate ()
            {
                base.Focus();
                base.SelectAll();
            });
        }

        public new void Focus()
        {
            BeginInvoke((MethodInvoker)delegate ()
            {
                base.Focus();
            });
        }

        public MaterialRichTextBox()
        {
            base.OnCreateControl();
            Multiline = true;

            BorderStyle = BorderStyle.None;
            Font = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
            BackColor = SkinManager.BackgroundColor;
            ForeColor = SkinManager.TextHighEmphasisColor;
            BackColorChanged += (sender, args) => BackColor = SkinManager.BackgroundColor;
            ForeColorChanged += (sender, args) => ForeColor = SkinManager.TextHighEmphasisColor;
        }
    }

    #endregion
}