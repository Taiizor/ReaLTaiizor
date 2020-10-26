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
            _horizontal = new ReaLTaiizor.Controls.PoisonScrollBar();
            _vertical = new ReaLTaiizor.Controls.PoisonScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            SuspendLayout();
            // 
            // _horizontal
            // 
            _horizontal.LargeChange = 10;
            _horizontal.Location = new System.Drawing.Point(0, 0);
            _horizontal.Maximum = 100;
            _horizontal.Minimum = 0;
            _horizontal.MouseWheelBarPartitions = 10;
            _horizontal.Name = "_horizontal";
            _horizontal.Orientation = Enum.Poison.ScrollOrientationType.Horizontal;
            _horizontal.ScrollbarSize = 50;
            _horizontal.Size = new System.Drawing.Size(200, 50);
            _horizontal.TabIndex = 0;
            _horizontal.UseSelectable = true;
            // 
            // _vertical
            // 
            _vertical.LargeChange = 10;
            _vertical.Location = new System.Drawing.Point(0, 0);
            _vertical.Maximum = 100;
            _vertical.Minimum = 0;
            _vertical.MouseWheelBarPartitions = 10;
            _vertical.Name = "_vertical";
            _vertical.Orientation = Enum.Poison.ScrollOrientationType.Vertical;
            _vertical.ScrollbarSize = 50;
            _vertical.Size = new System.Drawing.Size(50, 200);
            _vertical.TabIndex = 0;
            _vertical.UseSelectable = true;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonScrollBar _horizontal;
        private ReaLTaiizor.Controls.PoisonScrollBar _vertical;
    }
}