#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SpaceBorderLabel

    public class SpaceBorderLabel : SpaceControl // A Simple Label with a Border
    {
        public SpaceBorderLabel()
        {
            SetColor("DownGradient1", 42, 42, 42); // Basic Gradients Used to Shade the Button
            SetColor("DownGradient2", 42, 42, 42); // The Gradients are reversed, depending on if Button is Pressed or not
            SetColor("Text", 254, 254, 254); // The Color for the Text
            SetColor("Border1", 35, 35, 35); // The Inside Border
            SetColor("Border2", 42, 42, 42); // The Outside Border
            Size = new(120, 40);
        }

        private Color C1; // Set up Simple Colors
        private Color C2;
        private SolidBrush B1; // A Brush to use text
        private Pen P1; // A Pen used to create borders
        private Pen P2;
        private HorizontalAlignment _TextAlignment = HorizontalAlignment.Center;

        public HorizontalAlignment TextAlignment
        {
            get => _TextAlignment;
            set
            {
                _TextAlignment = value;
                Invalidate();
            }
        }

        protected override void ColorHook()
        { // Assign Variables
            C1 = GetColor("DownGradient1"); // Get the Colors for the Button Shading
            C2 = GetColor("DownGradient2");
            B1 = new(GetColor("Text")); // Set up Color for the Text
            P1 = new(GetColor("Border1")); // Get and create the borders for the Buttons
            P2 = new(GetColor("Border2"));
        }

        protected override void PaintHook()
        { // Draw Custom Label
            DrawGradient(C1, C2, ClientRectangle, 90f); // if button is pressed
            DrawText(B1, TextAlignment, 0, 0); // Draw the Text Smack dab in the middle of the button
            DrawBorders(P1, 1); // Create the Inner Border
            DrawBorders(P2); // Create the Outer Border
            DrawCorners(BackColor); // Draw the Corners
        }
    }

    #endregion
}