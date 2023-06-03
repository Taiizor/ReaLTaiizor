using System;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Nerator.CS.Character;
using static ReaLTaiizor.Nerator.CS.Generator;
using static ReaLTaiizor.Nerator.CS.Setting;
using static ReaLTaiizor.Nerator.CS.Variable;
using Clipboard = Skylark.Clipboard.Helper.Board;

namespace ReaLTaiizor.Nerator.UI
{
    public partial class EX : Form
    {
        public EX()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void Create_B_Click(object sender, EventArgs e)
        {
            Password.Text = Create(GetInt(PLength.Value.ToString(), PasswordLength, MinimumPasswordLength, MaximumPasswordLength), AlphabeticMode, SpecialMode);
        }

        private void Expand_CheckedChanged(object sender)
        {
            if (Expand.Checked)
            {
                Size = new Size(335, 595);
            }
            else
            {
                Size = new Size(335, 202);
            }

            EXExpandMode = Expand.Checked;
            CenterToScreen();
        }

        private void Copy_B_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Password.Text, true);
            Password.Focus();
        }

        private void TopMost_T_CheckedChanged(object sender)
        {
            TopMost = TopMost_T.Checked;
            TopMostMode = TopMost;
        }

        private void Alphabetic_CheckedChanged(object sender)
        {
            if (Just_Big.Checked)
            {
                AlphabeticMode = AlphabeticType.JB;
            }
            else if (Just_Small.Checked)
            {
                AlphabeticMode = AlphabeticType.JS;
            }
            else
            {
                AlphabeticMode = AlphabeticType.BS;
            }
        }

        private void Special_CheckedChanged(object sender)
        {
            if (Just_Number.Checked)
            {
                SpecialMode = SpecialType.JN;
            }
            else if (Just_Symbol.Checked)
            {
                SpecialMode = SpecialType.JS;
            }
            else
            {
                SpecialMode = SpecialType.NS;
            }
        }

        private void LoadConfig()
        {
            Expand.Checked = EXExpandMode;
            if (Expand.Checked)
            {
                Expand_CheckedChanged(null);
            }

            TopMost_T.Checked = TopMostMode;
            if (TopMost_T.Checked)
            {
                TopMost_T_CheckedChanged(null);
            }

            PLength.Value = PasswordLength;
            switch (AlphabeticMode)
            {
                case AlphabeticType.JB:
                    Just_Big.Checked = true;
                    break;
                case AlphabeticType.JS:
                    Just_Small.Checked = true;
                    break;
                default:
                    Big_Small.Checked = true;
                    break;
            }
            switch (SpecialMode)
            {
                case SpecialType.JN:
                    Just_Number.Checked = true;
                    break;
                case SpecialType.JS:
                    Just_Symbol.Checked = true;
                    break;
                default:
                    Number_Symbol.Checked = true;
                    break;
            }
        }

        private void EX_FormClosed(object sender, FormClosedEventArgs e)
        {
            PasswordLength = GetInt(PLength.Value.ToString(), PasswordLength, MinimumPasswordLength, MaximumPasswordLength);
            Save(ConfigFileName);
        }
    }
}