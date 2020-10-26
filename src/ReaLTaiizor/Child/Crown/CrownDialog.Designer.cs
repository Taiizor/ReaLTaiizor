using ReaLTaiizor.Controls;

namespace ReaLTaiizor.Child.Crown
{
    partial class CrownDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlFooter = new System.Windows.Forms.Panel();
            flowInner = new System.Windows.Forms.FlowLayoutPanel();
            btnOk = new CrownButton();
            btnCancel = new CrownButton();
            btnClose = new CrownButton();
            btnYes = new CrownButton();
            btnNo = new CrownButton();
            btnAbort = new CrownButton();
            btnRetry = new CrownButton();
            btnIgnore = new CrownButton();
            pnlFooter.SuspendLayout();
            flowInner.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(flowInner);
            pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlFooter.Location = new System.Drawing.Point(0, 357);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new System.Drawing.Size(767, 45);
            pnlFooter.TabIndex = 1;
            // 
            // flowInner
            // 
            flowInner.Controls.Add(btnOk);
            flowInner.Controls.Add(btnCancel);
            flowInner.Controls.Add(btnClose);
            flowInner.Controls.Add(btnYes);
            flowInner.Controls.Add(btnNo);
            flowInner.Controls.Add(btnAbort);
            flowInner.Controls.Add(btnRetry);
            flowInner.Controls.Add(btnIgnore);
            flowInner.Dock = System.Windows.Forms.DockStyle.Right;
            flowInner.Location = new System.Drawing.Point(104, 0);
            flowInner.Name = "flowInner";
            flowInner.Padding = new System.Windows.Forms.Padding(10);
            flowInner.Size = new System.Drawing.Size(663, 45);
            flowInner.TabIndex = 10014;
            // 
            // btnOk
            // 
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(10, 10);
            btnOk.Margin = new System.Windows.Forms.Padding(0);
            btnOk.Name = "btnOk";
            btnOk.Padding = new System.Windows.Forms.Padding(5);
            btnOk.Size = new System.Drawing.Size(75, 26);
            btnOk.TabIndex = 3;
            btnOk.Text = "Ok";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(85, 10);
            btnCancel.Margin = new System.Windows.Forms.Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(5);
            btnCancel.Size = new System.Drawing.Size(75, 26);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            // 
            // btnClose
            // 
            btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnClose.Location = new System.Drawing.Point(160, 10);
            btnClose.Margin = new System.Windows.Forms.Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Padding = new System.Windows.Forms.Padding(5);
            btnClose.Size = new System.Drawing.Size(75, 26);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            // 
            // btnYes
            // 
            btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            btnYes.Location = new System.Drawing.Point(235, 10);
            btnYes.Margin = new System.Windows.Forms.Padding(0);
            btnYes.Name = "btnYes";
            btnYes.Padding = new System.Windows.Forms.Padding(5);
            btnYes.Size = new System.Drawing.Size(75, 26);
            btnYes.TabIndex = 6;
            btnYes.Text = "Yes";
            // 
            // btnNo
            // 
            btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            btnNo.Location = new System.Drawing.Point(310, 10);
            btnNo.Margin = new System.Windows.Forms.Padding(0);
            btnNo.Name = "btnNo";
            btnNo.Padding = new System.Windows.Forms.Padding(5);
            btnNo.Size = new System.Drawing.Size(75, 26);
            btnNo.TabIndex = 7;
            btnNo.Text = "No";
            // 
            // btnAbort
            // 
            btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            btnAbort.Location = new System.Drawing.Point(385, 10);
            btnAbort.Margin = new System.Windows.Forms.Padding(0);
            btnAbort.Name = "btnAbort";
            btnAbort.Padding = new System.Windows.Forms.Padding(5);
            btnAbort.Size = new System.Drawing.Size(75, 26);
            btnAbort.TabIndex = 8;
            btnAbort.Text = "Abort";
            // 
            // btnRetry
            // 
            btnRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            btnRetry.Location = new System.Drawing.Point(460, 10);
            btnRetry.Margin = new System.Windows.Forms.Padding(0);
            btnRetry.Name = "btnRetry";
            btnRetry.Padding = new System.Windows.Forms.Padding(5);
            btnRetry.Size = new System.Drawing.Size(75, 26);
            btnRetry.TabIndex = 9;
            btnRetry.Text = "Retry";
            // 
            // btnIgnore
            // 
            btnIgnore.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            btnIgnore.Location = new System.Drawing.Point(535, 10);
            btnIgnore.Margin = new System.Windows.Forms.Padding(0);
            btnIgnore.Name = "btnIgnore";
            btnIgnore.Padding = new System.Windows.Forms.Padding(5);
            btnIgnore.Size = new System.Drawing.Size(75, 26);
            btnIgnore.TabIndex = 10;
            btnIgnore.Text = "Ignore";
            // 
            // CrownDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(767, 402);
            Controls.Add(pnlFooter);
            Name = "CrownDialog";
            Text = "CrownDialog";
            pnlFooter.ResumeLayout(false);
            flowInner.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.FlowLayoutPanel flowInner;
    }
}