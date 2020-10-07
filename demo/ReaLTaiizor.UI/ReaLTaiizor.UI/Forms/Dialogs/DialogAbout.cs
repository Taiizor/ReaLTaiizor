using System.Windows.Forms;
using ReaLTaiizor.Child.Crown;

namespace ReaLTaiizor.UI.Forms.Dialogs
{
    public partial class DialogAbout : CrownDialog
    {
        #region Constructor Region

        public DialogAbout()
        {
            InitializeComponent();

            lblVersion.Text = $"Version: {Application.ProductVersion.ToString()}";
            btnOk.Text = "Close";
        }

        #endregion
    }
}