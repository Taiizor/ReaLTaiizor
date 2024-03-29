﻿using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;

namespace ReaLTaiizor_CR
{
    public partial class Material : MaterialForm
    {
        private readonly MaterialSkinManager MManager;

        private int colorSchemeIndex;

        public Material()
        {
            InitializeComponent();

            MManager = MaterialSkinManager.Instance;

            MManager.AddFormToManage(this);

            MManager.Theme = MaterialSkinManager.Themes.DARK;

            //MManager.ColorScheme = new MaterialColorScheme(0x00C926b3, 0xA1008B, 0xDC2EFF, 0x006E70FF, MaterialTextShade.LIGHT);
            //MManager.ColorScheme = new MaterialColorScheme("#00480157", "#370142", "DC2EFF", "00BB5FCF", MaterialTextShade.LIGHT);
            MManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.BlueGrey800, MaterialPrimary.BlueGrey900, MaterialPrimary.BlueGrey500, MaterialAccent.LightBlue200, MaterialTextShade.LIGHT);
        }

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Fast;
        }

        private void MaterialButton2_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Normal;
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            //MManager.Theme = MManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;

            colorSchemeIndex++;

            if (colorSchemeIndex > 2)
            {
                colorSchemeIndex = 0;
            }

            UpdateColor();
        }

        private void UpdateColor()
        {
            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    MManager.ColorScheme = new MaterialColorScheme(
                        MManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialPrimary.Teal500 : MaterialPrimary.Indigo500,
                        MManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialPrimary.Teal700 : MaterialPrimary.Indigo700,
                        MManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialPrimary.Teal200 : MaterialPrimary.Indigo100,
                        MaterialAccent.Pink200,
                        MManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialTextShade.LIGHT : MaterialTextShade.DARK);
                    break;
                case 1:
                    MManager.Theme = MaterialSkinManager.Themes.LIGHT;
                    MManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Green600,
                        MaterialPrimary.Green700,
                        MaterialPrimary.Green200,
                        MaterialAccent.Red100,
                        MaterialTextShade.LIGHT);
                    break;
                case 2:
                    MManager.Theme = MaterialSkinManager.Themes.DARK;
                    MManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.BlueGrey800,
                        MaterialPrimary.BlueGrey900,
                        MaterialPrimary.BlueGrey500,
                        MaterialAccent.LightBlue200,
                        MaterialTextShade.LIGHT);
                    break;
            }

            Invalidate();
            Refresh();
        }
    }
}