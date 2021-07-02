#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region SpaceForm

    public class SpaceForm : SpaceLibrary
    {
        public SpaceForm()
        {
            BackColor = Color.FromArgb(42, 42, 42); // Background Color
            TransparencyKey = Color.Purple; // The Color used for Transparent
            SetColor("Background", 42, 42, 42); // Background Color
            SetColor("DarkGradient", 32, 32, 32); // Used to Make a Gradient
            SetColor("BackgroundGradient", 42, 42, 42); // Used to make a Gradient
            SetColor("Line1", 42, 42, 42); // First Line Color
            SetColor("Line2", 28, 28, 28); // Second Line Color
            SetColor("Text", 254, 254, 254);// Text Color
            SetColor("Border1", 43, 43, 43); // First Border
            SetColor("Border2", 25, 25, 25); // Second Borders
            MinimumSize = new(200, 25);
            Padding = new Padding(5, 25, 5, 5);
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Declare some Variables
        // The Letter is Variable type, while the number is what color it is
        private Color C1;
        private Color C2;
        private Color C3;
        private Pen P1;
        private Pen P2;
        private Pen P3;
        private Pen P4;
        private SolidBrush B1;

        protected override void ColorHook()
        {
            C1 = GetColor("Background"); // Get the Background Color
            C2 = GetColor("DarkGradient"); // Get the Dark Gradient
            C3 = GetColor("BackgroundGradient"); // The Light Gradient
            P1 = new(GetColor("Line1")); // Create a Pen for the Line
            P2 = new(GetColor("Line2"));
            P3 = new(GetColor("Border1")); // Create a Pen for the Border
            P4 = new(GetColor("Border2"));
            B1 = new(GetColor("Text")); // Set up a brush for the Text
            BackColor = C1; // Create a Second Variable for The Background Color
        }

        protected override void PaintHook()
        {
            G.Clear(C1); // Clear the Form with the Basic Color
            DrawGradient(C3, C2, 0, 0, Width, 25); // Draw the Background  Gradient
            G.DrawLine(P1, 0, 25, Width, 25); // Draw the Separtor for the Bar
            G.DrawLine(P2, 0, 25, Width, 25);
            DrawText(B1, HorizontalAlignment.Left, 5, 0); // Draw the Title Text
            DrawBorders(P3, 1); // Creating the Inner Border
            DrawBorders(P4); // Create the Outer Border
            DrawCorners(TransparencyKey); // Create a Corner with the Transparent Key
        }
    }

    #endregion
}