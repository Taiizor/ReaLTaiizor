namespace ReaLTaiizor.Child.Poison
{
    partial class PoisonMessageBoxControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Body = new System.Windows.Forms.Panel();
            tlpBody = new System.Windows.Forms.TableLayoutPanel();
            messageLabel = new System.Windows.Forms.Label();
            titleLabel = new System.Windows.Forms.Label();
            pnlBottom = new System.Windows.Forms.Panel();
            poisonButton2 = new ReaLTaiizor.Controls.PoisonButton();
            poisonButton1 = new ReaLTaiizor.Controls.PoisonButton();
            poisonButton3 = new ReaLTaiizor.Controls.PoisonButton();
            Body.SuspendLayout();
            tlpBody.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelbody
            // 
            Body.BackColor = System.Drawing.Color.DarkGray;
            Body.Controls.Add(tlpBody);
            Body.Dock = System.Windows.Forms.DockStyle.Fill;
            Body.Location = new System.Drawing.Point(0, 0);
            Body.Margin = new System.Windows.Forms.Padding(0);
            Body.Name = "panelbody";
            Body.Size = new System.Drawing.Size(804, 211);
            Body.TabIndex = 2;
            // 
            // tlpBody
            // 
            tlpBody.ColumnCount = 3;
            tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tlpBody.Controls.Add(messageLabel, 1, 2);
            tlpBody.Controls.Add(titleLabel, 1, 1);
            tlpBody.Controls.Add(pnlBottom, 1, 3);
            tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpBody.Location = new System.Drawing.Point(0, 0);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 4;
            tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tlpBody.Size = new System.Drawing.Size(804, 211);
            tlpBody.TabIndex = 6;
            // 
            // messageLabel
            // 
            messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            messageLabel.BackColor = System.Drawing.Color.Transparent;
            messageLabel.ForeColor = System.Drawing.Color.White;
            messageLabel.Location = new System.Drawing.Point(83, 37);
            messageLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new System.Drawing.Size(640, 134);
            messageLabel.TabIndex = 0;
            messageLabel.Text = "message here";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.BackColor = System.Drawing.Color.Transparent;
            titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            titleLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            titleLabel.Location = new System.Drawing.Point(80, 5);
            titleLabel.Margin = new System.Windows.Forms.Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(159, 32);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "message title";
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = System.Drawing.Color.Transparent;
            pnlBottom.Controls.Add(poisonButton2);
            pnlBottom.Controls.Add(poisonButton1);
            pnlBottom.Controls.Add(poisonButton3);
            pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBottom.Location = new System.Drawing.Point(80, 171);
            pnlBottom.Margin = new System.Windows.Forms.Padding(0);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new System.Drawing.Size(643, 40);
            pnlBottom.TabIndex = 2;
            // 
            // poisonButton2
            // 
            poisonButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            poisonButton2.FontWeight = Extension.Poison.PoisonButtonWeight.Regular;
            poisonButton2.Location = new System.Drawing.Point(455, 1);
            poisonButton2.Name = "poisonButton2";
            poisonButton2.Size = new System.Drawing.Size(90, 26);
            poisonButton2.TabIndex = 4;
            poisonButton2.Text = "button 2";
            poisonButton2.UseSelectable = true;
            // 
            // poisonButton1
            // 
            poisonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            poisonButton1.BackColor = System.Drawing.Color.ForestGreen;
            poisonButton1.FontWeight = Extension.Poison.PoisonButtonWeight.Regular;
            poisonButton1.Location = new System.Drawing.Point(357, 1);
            poisonButton1.Name = "poisonButton1";
            poisonButton1.Size = new System.Drawing.Size(90, 26);
            poisonButton1.TabIndex = 3;
            poisonButton1.Text = "button 1";
            poisonButton1.UseSelectable = true;
            poisonButton1.UseVisualStyleBackColor = false;
            // 
            // poisonButton3
            // 
            poisonButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            poisonButton3.FontWeight = Extension.Poison.PoisonButtonWeight.Regular;
            poisonButton3.Location = new System.Drawing.Point(553, 1);
            poisonButton3.Name = "poisonButton3";
            poisonButton3.Size = new System.Drawing.Size(90, 26);
            poisonButton3.TabIndex = 5;
            poisonButton3.Text = "button 3";
            poisonButton3.UseSelectable = true;
            // 
            // PoisonMessageBoxControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 28F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(804, 211);
            ControlBox = false;
            Controls.Add(Body);
            Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "PoisonMessageBoxControl";
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Body.ResumeLayout(false);
            tlpBody.ResumeLayout(false);
            tlpBody.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label messageLabel;
        private ReaLTaiizor.Controls.PoisonButton poisonButton1;
        private ReaLTaiizor.Controls.PoisonButton poisonButton2;
        private ReaLTaiizor.Controls.PoisonButton poisonButton3;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private System.Windows.Forms.Panel pnlBottom;
    }
}