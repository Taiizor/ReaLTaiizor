using System;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Colors;

namespace ReaLTaiizor_CR
{
    public partial class Material : MaterialForm
    {
        private readonly MaterialManager MManager;

        private int colorSchemeIndex;

        public Material()
        {
            InitializeComponent();

            MManager = MaterialManager.Instance;
            MManager.AddFormToManage(this);
            MManager.Theme = MaterialManager.Themes.DARK;
            MManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.BlueGrey800, MaterialPrimary.BlueGrey900, MaterialPrimary.BlueGrey500, MaterialAccent.LightBlue200, MaterialTextShade.WHITE);
        }

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Fast;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Normal;
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            //MManager.Theme = MManager.Theme == MaterialManager.Themes.DARK ? MaterialManager.Themes.LIGHT : MaterialManager.Themes.DARK;

            colorSchemeIndex++;
            if (colorSchemeIndex > 2)
                colorSchemeIndex = 0;
            updateColor();
        }

        private void updateColor()
        {
            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    MManager.ColorScheme = new MaterialColorScheme(
                        MManager.Theme == MaterialManager.Themes.DARK ? MaterialPrimary.Teal500 : MaterialPrimary.Indigo500,
                        MManager.Theme == MaterialManager.Themes.DARK ? MaterialPrimary.Teal700 : MaterialPrimary.Indigo700,
                        MManager.Theme == MaterialManager.Themes.DARK ? MaterialPrimary.Teal200 : MaterialPrimary.Indigo100,
                        MaterialAccent.Pink200,
                        MaterialTextShade.WHITE);
                    break;
                case 1:
                    MManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Green600,
                        MaterialPrimary.Green700,
                        MaterialPrimary.Green200,
                        MaterialAccent.Red100,
                        MaterialTextShade.WHITE);
                    break;
                case 2:
                    MManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.BlueGrey800,
                        MaterialPrimary.BlueGrey900,
                        MaterialPrimary.BlueGrey500,
                        MaterialAccent.LightBlue200,
                        MaterialTextShade.WHITE);
                    break;
            }
            Invalidate();
        }
    }
}