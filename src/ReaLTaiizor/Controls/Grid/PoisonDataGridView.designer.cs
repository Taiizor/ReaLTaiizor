namespace ReaLTaiizor.Controls
{
    partial class PoisonDataGridView
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
            this._horizontal = new ReaLTaiizor.Controls.PoisonScrollBar();
            this._vertical = new ReaLTaiizor.Controls.PoisonScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // _horizontal
            // 
            this._horizontal.LargeChange = 10;
            this._horizontal.Location = new System.Drawing.Point(0, 0);
            this._horizontal.Maximum = 100;
            this._horizontal.Minimum = 0;
            this._horizontal.MouseWheelBarPartitions = 10;
            this._horizontal.Name = "_horizontal";
            this._horizontal.Orientation = Enum.Poison.ScrollOrientationType.Horizontal;
            this._horizontal.ScrollbarSize = 50;
            this._horizontal.Size = new System.Drawing.Size(200, 50);
            this._horizontal.TabIndex = 0;
            this._horizontal.UseSelectable = true;
            // 
            // _vertical
            // 
            this._vertical.LargeChange = 10;
            this._vertical.Location = new System.Drawing.Point(0, 0);
            this._vertical.Maximum = 100;
            this._vertical.Minimum = 0;
            this._vertical.MouseWheelBarPartitions = 10;
            this._vertical.Name = "_vertical";
            this._vertical.Orientation = Enum.Poison.ScrollOrientationType.Vertical;
            this._vertical.ScrollbarSize = 50;
            this._vertical.Size = new System.Drawing.Size(50, 200);
            this._vertical.TabIndex = 0;
            this._vertical.UseSelectable = true;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonScrollBar _horizontal;
        private ReaLTaiizor.Controls.PoisonScrollBar _vertical;
    }
}