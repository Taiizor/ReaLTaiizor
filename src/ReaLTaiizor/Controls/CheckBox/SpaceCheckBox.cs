#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SpaceCheckBox

    [DefaultEvent("CheckedChanged")] // This is used to see if it is a checkbox and so on.
    public class SpaceCheckBox : SpaceControl // This Checkbox uses a green middle
    {
        public SpaceCheckBox()
        {
            Transparent = true; // it is infact transpernt
            BackColor = Color.Transparent;
            // no lockheight since that way they can choose how they want it.
            // no text since i want the user to choose wether or not they want check, also, that way the border doesn't go around it.
            SetColor("Background", 249, 249, 249); // Background is Dark
            SetColor("ClickedGradient1", 204, 201, 35); // the green graident on the inside.
            SetColor("ClickedGradient2", 140, 138, 27);
            SetColor("Border1", 235, 235, 235); // The Inside Border
            SetColor("Border2", 249, 249, 249); // The Outside Border
            Cursor = Cursors.Hand;
        }

        // set up the variables
        private Color C1; // Set up Simple Colors
        private Color C2;
        private Color C3;
        private Pen P1; // A Pen used to create borders
        private Pen P2;

        protected override void ColorHook()
        {
            C1 = GetColor("Background"); // Get the Colors for the Button Shading
            C2 = GetColor("ClickedGradient1");
            C3 = GetColor("ClickedGradient2");
            P1 = new(GetColor("Border1")); // Get and create the borders for the Buttons
            P2 = new(GetColor("Border2"));
        }

        protected override void PaintHook()
        {
            G.Clear(BackColor);
            switch (_Checked)
            {
                case true:
                    //Put your checked state here
                    DrawGradient(C1, C1, ClientRectangle, 90f); // checked background
                    DrawGradient(C2, C3, 3, 3, Width - 6, Height - 6, 90f); // checked background
                    DrawBorders(P1, 1); // Create the Inner Border
                    Height = Width;
                    DrawBorders(P2); // Create the Outer Border
                    DrawCorners(BackColor); // Draw the Corners
                    break;
                case false:
                    //Put your unchecked state here
                    DrawGradient(C1, C1, ClientRectangle, 90f); // unchecked background
                    DrawBorders(P1, 1); // Create the Inner Border
                    Height = Width;
                    DrawBorders(P2); // Create the Outer Border
                    DrawCorners(BackColor); // Draw the Corners
                    break;
            }
        }

        private bool _Checked { get; set; }
        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                CheckedChanged?.Invoke(this);
            }
        }

        protected override void OnClick(System.EventArgs e)
        {
            Checked = !Checked;
            base.OnClick(e);
        }

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
    }

    #endregion
}