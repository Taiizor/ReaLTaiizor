using Skylark.Standard.Extension.Hash;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Clipboard = Skylark.Clipboard.Helper.Board;

namespace ReaLTaiizor.Hashing
{
    public partial class Hashing : Form
    {
        public Hashing()
        {
            InitializeComponent();

            FilePath.Text = Application.ExecutablePath;
        }

        private void CopyMD5_Click(object sender, EventArgs e)
        {
            if (ResultMD5.Text != Clipboard.GetText())
            {
                Clipboard.SetDataObject(ResultMD5.Text, true);
                if (ResultMD5.Text == Clipboard.GetText())
                {
                    ResultMD5.Focus();
                }
            }
        }

        private void CopySHA1_Click(object sender, EventArgs e)
        {
            if (ResultSHA1.Text != Clipboard.GetText())
            {
                Clipboard.SetDataObject(ResultSHA1.Text, true);
                if (ResultSHA1.Text == Clipboard.GetText())
                {
                    ResultSHA1.Focus();
                }
            }
        }

        private void CopySHA256_Click(object sender, EventArgs e)
        {
            if (ResultSHA256.Text != Clipboard.GetText())
            {
                Clipboard.SetDataObject(ResultSHA256.Text, true);
                if (ResultSHA256.Text == Clipboard.GetText())
                {
                    ResultSHA256.Focus();
                }
            }
        }

        private void CopySHA384_Click(object sender, EventArgs e)
        {
            if (ResultSHA384.Text != Clipboard.GetText())
            {
                Clipboard.SetDataObject(ResultSHA384.Text, true);
                if (ResultSHA384.Text == Clipboard.GetText())
                {
                    ResultSHA384.Focus();
                }
            }
        }

        private void CopySHA512_Click(object sender, EventArgs e)
        {
            if (ResultSHA512.Text != Clipboard.GetText())
            {
                Clipboard.SetDataObject(ResultSHA512.Text, true);
                if (ResultSHA512.Text == Clipboard.GetText())
                {
                    ResultSHA512.Focus();
                }
            }
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Title = "Please choose any file.";

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath.Text = OpenFileDialog.FileName;
            }
        }

        private void HashFile_Click(object sender, EventArgs e)
        {
            try
            {
                CopyMD5.Enabled = false;
                CopySHA1.Enabled = false;
                CopySHA256.Enabled = false;
                CopySHA384.Enabled = false;
                CopySHA512.Enabled = false;

                HashFile.Enabled = false;
                HashAsyncFile.Enabled = false;

                Separator.LineColor = Color.Crimson;

                if (File.Exists(FilePath.Text))
                {
                    ResultMD5.Text = HashExtension.FileToMD5(FilePath.Text, Upper: true, Invariant: true);
                    CopyMD5.Enabled = true;

                    ResultSHA1.Text = HashExtension.FileToSHA1(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA1.Enabled = true;

                    ResultSHA256.Text = HashExtension.FileToSHA256(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA256.Enabled = true;

                    ResultSHA384.Text = HashExtension.FileToSHA384(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA384.Enabled = true;

                    ResultSHA512.Text = HashExtension.FileToSHA512(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA512.Enabled = true;

                    Separator.LineColor = Color.SeaGreen;
                }
                else
                {
                    MessageBox.Show("Please select a valid file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HashFile.Enabled = true;
                HashAsyncFile.Enabled = true;
            }
        }

        private async void HashAsyncFile_Click(object sender, EventArgs e)
        {
            try
            {
                CopyMD5.Enabled = false;
                CopySHA1.Enabled = false;
                CopySHA256.Enabled = false;
                CopySHA384.Enabled = false;
                CopySHA512.Enabled = false;

                HashFile.Enabled = false;
                HashAsyncFile.Enabled = false;

                Separator.LineColor = Color.Crimson;

                if (File.Exists(FilePath.Text))
                {
                    ResultMD5.Text = await HashExtension.FileToMD5Async(FilePath.Text, Upper: true, Invariant: true);
                    CopyMD5.Enabled = true;

                    ResultSHA1.Text = await HashExtension.FileToSHA1Async(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA1.Enabled = true;

                    ResultSHA256.Text = await HashExtension.FileToSHA256Async(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA256.Enabled = true;

                    ResultSHA384.Text = await HashExtension.FileToSHA384Async(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA384.Enabled = true;

                    ResultSHA512.Text = await HashExtension.FileToSHA512Async(FilePath.Text, Upper: true, Invariant: true);
                    CopySHA512.Enabled = true;

                    Separator.LineColor = Color.SeaGreen;
                }
                else
                {
                    MessageBox.Show("Please select a valid file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HashFile.Enabled = true;
                HashAsyncFile.Enabled = true;
            }
        }
    }
}