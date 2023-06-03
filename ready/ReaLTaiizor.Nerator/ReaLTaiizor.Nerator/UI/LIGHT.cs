using ReaLTaiizor.Forms;
using ReaLTaiizor.Nerator.CS;
using ReaLTaiizor.Nerator.UC.LIGHT.HISTORY;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static ReaLTaiizor.Nerator.CS.Character;
using static ReaLTaiizor.Nerator.CS.Generator;
using static ReaLTaiizor.Nerator.CS.History;
using static ReaLTaiizor.Nerator.CS.Page;
using static ReaLTaiizor.Nerator.CS.Setting;
using static ReaLTaiizor.Nerator.CS.Strength;
using static ReaLTaiizor.Nerator.CS.Variable;
using Clipboard = Skylark.Clipboard.Helper.Board;

namespace ReaLTaiizor.Nerator.UI
{
    public partial class LIGHT : MaterialForm
    {
        public LIGHT()
        {
            InitializeComponent();
            LoadConfig();
            HistoryLoad();
        }

        private void CEB_Click(object sender, EventArgs e)
        {
            string GP = Create(GetInt(PWLN.Value.ToString(), PasswordLength, MinimumPasswordLength, MaximumPasswordLength), AlphabeticMode, SpecialMode);
            PWDTB.Text = GP;
            if (HistoryMode)
            {
                Add(HistoryFileName, GP, DefaultDateTime);
                HistoryAdd(GP, GetTime(DefaultDateTime, DefaultDateTime), GetDate(DefaultDateTime, DefaultDateTime));
            }
            PLPB.Value = StrengthMode(CheckScore2(GP));
            PLPB.Style = StyleMode(PLPB.Value);
            Status.Message = "New password generation completed successfully!";
        }

        private void CYB_Click(object sender, EventArgs e)
        {
            if (PWDTB.Text != Clipboard.GetText())
            {
                Clipboard.SetDataObject(PWDTB.Text, true);
                if (PWDTB.Text == Clipboard.GetText())
                {
                    Status.Message = "Generated password copied successfully!";
                    PWDTB.Focus();
                }
                else
                {
                    Status.Message = "Failed to copy the generated password!";
                }
            }
        }

        private void AMCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (GetAlphabetic(AlphabeticMode) != AMCB.SelectedItem.ToString())
                {
                    AlphabeticMode = GetAlphabetic(AMCB.SelectedItem, AlphabeticType.BS);
                    Status.Message = "Alphabetical mode changed to " + AMCB.SelectedItem.ToString() + ".";
                }
            }
            catch (Exception Ex)
            {
                Status.Message = "Error - " + Ex.Source + ": " + Ex.Message;
            }
        }

        private void SMCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (GetSpecial(SpecialMode) != SMCB.SelectedItem.ToString())
                {
                    SpecialMode = GetSpecial(SMCB.SelectedItem, SpecialType.NS);
                    Status.Message = "Special mode changed to " + SMCB.SelectedItem.ToString() + ".";
                }
            }
            catch (Exception Ex)
            {
                Status.Message = "Error - " + Ex.Source + ": " + Ex.Message;
            }
        }

        private void HYS_CheckedChanged(object sender, EventArgs e)
        {
            HistoryMode = HYS.Checked;

            if (HYS.Checked)
            {
                Status.Message = "Generated passwords will be saved in the history.";
            }
            else
            {
                Status.Message = "Generated passwords will not be saved in the history.";
            }
        }

        private void TMCB_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = TMCB.Checked;
            TopMostMode = TopMost;

            if (TMCB.Checked)
            {
                Status.Message = "Nerator has been successfully pinned to the top.";
            }
            else
            {
                Status.Message = "Nerator has been restored to normal priority level.";
            }
        }

        private void HistoryAdd(string Password, string Time, string Date, DockStyle Style = DockStyle.Top)
        {
            HYP.Controls.Add(new PWD(Password, Time, Date) { Dock = Style });
        }

        private void HistoryLoad()
        {
            if (HYP.Controls.Count > 1)
            {
                HYP.Controls.Clear();
            }

            _ = new History(HistoryFileName);

            Dictionary<string, string> History = Loader(HistoryFileName);

            foreach (string PKey in History.Keys)
            {
                if (++ListPasswordCount <= MaximumHistoryList)
                {
                    HistoryAdd(PKey, GetTime(GetLong(History[PKey], DefaultDateTime), DefaultDateTime), GetDate(GetLong(History[PKey], DefaultDateTime), DefaultDateTime));
                }
                else
                {
                    break;
                }
            }

            History.Clear();
        }

        private void STATUST_Tick(object sender, EventArgs e)
        {
            try
            {
                long Result = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - Status.ChangedStatus;
                if (Result >= 3)
                {
                    Status.Message = Status.DefaultStatus;
                }
            }
            catch (Exception Ex)
            {
                Status.Message = "Error - " + Ex.Source + ": " + Ex.Message;
            }
        }

        private void STATUSMT_Tick(object sender, EventArgs e)
        {
            try
            {
                SSBR.Text = Status.Message;
                if (PWLN.Value < MinimumPasswordLength)
                {
                    PWLN.Value = MinimumPasswordLength;
                }
                else if (PWLN.Value > MaximumPasswordLength)
                {
                    PWLN.Value = MaximumPasswordLength;
                }
            }
            catch (Exception Ex)
            {
                Status.Message = "Error - " + Ex.Source + ": " + Ex.Message;
            }
        }

        private void LoadConfig()
        {
            HYS.Checked = HistoryMode;

            TMCB.Checked = TopMostMode;
            TopMost = TopMostMode;

            SMCB.SelectedIndex = SMCB.Items.IndexOf(GetSpecial(SpecialMode));
            AMCB.SelectedIndex = AMCB.Items.IndexOf(GetAlphabetic(AlphabeticMode));

            PWLN.Value = PasswordLength;
            MTC.SelectedTab = OpenPageMode(PageMode);
            MTS.BaseTabControl = MTC;

            Invalidate();
        }

        private TabPage OpenPageMode(PageType Type)
        {
            return Type switch
            {
                PageType.History => History,
                PageType.Setting => Setting,
                _ => Generate,
            };
        }

        private void LIGHT_FormClosed(object sender, FormClosedEventArgs e)
        {
            PageMode = GetPageMode(MTC.SelectedTab.Text);
            PasswordLength = GetInt(PWLN.Value.ToString(), PasswordLength, MinimumPasswordLength, MaximumPasswordLength);
            Save(ConfigFileName);
        }
    }
}