#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SpaceSeparatorHorizontal

    public class SpaceSeparatorHorizontal : SpaceControl // A Horizontal Separator
    {
        public SpaceSeparatorHorizontal()
        {
            SetColor("DownGradient1", 42, 42, 42); // Basic Gradients Used to Shade the Button
            SetColor("DownGradient2", 42, 42, 42); // The Gradients are reversed, depending on if Button is Pressed or not
            SetColor("Border1", 35, 35, 35); // The Inside Border
            SetColor("Border2", 42, 42, 42); // The Outside Border
            Size = new(50, 4);
        }

        private Color C1; // Set up Simple Colors
        private Color C2;
        private Pen P1; // A Pen used to create borders
        private Pen P2;

        protected override void ColorHook()
        { // Assign Variables
            C1 = GetColor("DownGradient1"); // Get the Colors for the Button Shading
            C2 = GetColor("DownGradient2");
            P1 = new(GetColor("Border1")); // Get and create the borders for the Buttons
            P2 = new(GetColor("Border2"));
        }

        protected override void PaintHook()
        { // Draw Custom Label
            DrawGradient(C1, C2, ClientRectangle, 90f); // if button is pressed
            DrawBorders(P1, 1); // Create the Inner Border
            DrawBorders(P2); // Create the Outer Border
            DrawCorners(BackColor); // Draw the Corners
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // EDIT: ADD AN EXTRA HEIGHT VALIDATION TO AVOID INITIALIZATION PROBLEMS
            // BITWISE 'AND' OPERATION: IF ZERO THEN HEIGHT IS NOT INVOLVED IN THIS OPERATION
            if ((specified & BoundsSpecified.Height) == 0 || height == 4)
            {
                base.SetBoundsCore(x, y, width, 4, specified);
            }
            else
            {
                return; // RETURN WITHOUT DOING ANY RESIZING
            }
        }
    }

    #endregion
}
