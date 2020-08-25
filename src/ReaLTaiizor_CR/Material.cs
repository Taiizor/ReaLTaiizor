﻿using ReaLTaiizor.Utils;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Colors;

namespace ReaLTaiizor_CR
{
    public partial class Material : MaterialForm
    {
        public Material()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.BlueGrey800, MaterialPrimary.BlueGrey900, MaterialPrimary.BlueGrey500, MaterialAccent.LightBlue200, MaterialTextShade.WHITE);
        }
    }
}