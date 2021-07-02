#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SpaceButton

    public class SpaceButton : SpaceControl // A Simple Button
    {
        public SpaceButton()
        {
            SetColor("DownGradient1", 42, 42, 42); // Basic Gradients Used to Shade the Button
            SetColor("DownGradient2", 50, 50, 50); // The Gradients are reversed, depending on if Button is Pressed or not
            SetColor("NoneGradient1", 50, 50, 50);
            SetColor("NoneGradient2", 42, 42, 42);
            SetColor("ClickedGradient1", 47, 47, 47);
            SetColor("ClickedGradient2", 39, 39, 39);
            SetColor("Text", 254, 254, 254); // The Color for the Text
            SetColor("Border1", 35, 35, 35); // The Inside Border
            SetColor("Border2", 42, 42, 42); // The Outside Border
            Cursor = Cursors.Hand;
            Size = new(120, 40);
        }

        private Color C1; // Set up Simple Colors
        private Color C2;
        private Color C3;
        private Color C4;
        private Color C5;
        private Color C6;
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
        {
            C1 = GetColor("DownGradient1"); // Get the Colors for the Button Shading
            C2 = GetColor("DownGradient2");
            C3 = GetColor("NoneGradient1");
            C4 = GetColor("NoneGradient2");
            C5 = GetColor("ClickedGradient1");
            C6 = GetColor("ClickedGradient2");
            B1 = new(GetColor("Text")); // Set up Color for the Text
            P1 = new(GetColor("Border1")); // Get and create the borders for the Buttons
            P2 = new(GetColor("Border2"));
        }

        protected override void PaintHook()
        {
            if (State == MouseStateSpace.Over)
            {
                DrawGradient(C1, C2, ClientRectangle, 90f); // if button is hovered over
            }
            else if (State == MouseStateSpace.Down)
            {
                DrawGradient(C6, C5, ClientRectangle, 90f);
            }
            else
            {
                DrawGradient(C3, C4, ClientRectangle, 90f); // else change the shading
            }

            DrawText(B1, TextAlignment, 0, 0); // Draw the Text Smack dab in the middle of the button
            DrawBorders(P1, 1); // Create the Inner Border
            DrawBorders(P2); // Create the Outer Border
            DrawCorners(BackColor); // Draw the Corners
        }
    }

    #endregion
}