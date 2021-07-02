using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Util;

namespace ReaLTaiizor.XAMPP
{
    public partial class XAMPP : PoisonForm
    {
        private readonly MaterialManager MM;

        public XAMPP()
        {
            InitializeComponent();
            MM = MaterialManager.Instance;
            MM.EnforceBackcolorOnAllComponents = true;
            MM.Theme = MaterialManager.Themes.DARK;
            MM.ColorScheme = new(MaterialPrimary.Grey900, MaterialPrimary.Grey700, MaterialPrimary.Grey500, MaterialAccent.Orange400, MaterialTextShade.WHITE);
        }
    }
}