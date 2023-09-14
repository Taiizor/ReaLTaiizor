using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Util;
using ReaLTaiizor.Manager;

namespace ReaLTaiizor.XAMPP
{
    public partial class XAMPP : PoisonForm
    {
        private readonly MaterialSkinManager MM;

        public XAMPP()
        {
            InitializeComponent();
            MM = MaterialSkinManager.Instance;
            MM.EnforceBackcolorOnAllComponents = true;
            MM.Theme = MaterialSkinManager.Themes.DARK;
            MM.ColorScheme = new(MaterialPrimary.Grey900, MaterialPrimary.Grey700, MaterialPrimary.Grey500, MaterialAccent.Orange400, MaterialTextShade.WHITE);
        }
    }
}