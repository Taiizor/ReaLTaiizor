#region Imports

using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialMessageBox

    public class MaterialMessageBox : MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        [Browsable(false)]
        public Point MouseLocation { get; set; }

        public static DialogResult Show(string text, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(IWin32Window owner, string text, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(string text, string caption, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(null, text, caption, buttons, icon, defaultButton, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, bool UseRichTextBox = true, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(owner, text, caption, buttons, icon, defaultButton, UseRichTextBox, buttonsPosition);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons messageBoxButtons, MaterialFlexibleForm.ButtonsPosition buttonsPosition = MaterialFlexibleForm.ButtonsPosition.Right)
        {
            return MaterialFlexibleForm.Show(null, text, caption, messageBoxButtons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, true, buttonsPosition);
        }
    }

    #endregion
}