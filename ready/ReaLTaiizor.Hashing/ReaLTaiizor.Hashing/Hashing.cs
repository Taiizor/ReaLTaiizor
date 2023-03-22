using Skylark.Standard.Extension.Hash;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ReaLTaiizor.Hashing
{
    public partial class Hashing : Form
    {
        public Hashing()
        {
            InitializeComponent();

            FilePath.Text = Application.ExecutablePath;
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
                HashFile.Enabled = false;
                HashAsyncFile.Enabled = false;

                Separator.LineColor = Color.Crimson;

                if (File.Exists(FilePath.Text))
                {
                    ResultMD5.Text = HashExtension.FileToMD5(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA1.Text = HashExtension.FileToSHA1(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA256.Text = HashExtension.FileToSHA256(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA384.Text = HashExtension.FileToSHA384(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA512.Text = HashExtension.FileToSHA512(FilePath.Text, Upper: true, Invariant: true);

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
                HashFile.Enabled = false;
                HashAsyncFile.Enabled = false;

                Separator.LineColor = Color.Crimson;

                if (File.Exists(FilePath.Text))
                {
                    ResultMD5.Text = await HashExtension.FileToMD5Async(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA1.Text = await HashExtension.FileToSHA1Async(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA256.Text = await HashExtension.FileToSHA256Async(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA384.Text = await HashExtension.FileToSHA384Async(FilePath.Text, Upper: true, Invariant: true);
                    ResultSHA512.Text = await HashExtension.FileToSHA512Async(FilePath.Text, Upper: true, Invariant: true);

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