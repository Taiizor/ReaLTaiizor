#region Imports

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotFormDropShadow

    public class ParrotFormDropShadow : Component
    {
        public ParrotFormDropShadow()
        {
            RefreshUI.Interval = 250;
            RefreshUI.Tick += RefreshUI_Tick;
            RefreshUI.Enabled = true;
        }

        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;

                if (value == null)
                {
                    return;
                }

                if (value.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
                {
                    IComponent rootComponent = designerHost.RootComponent;
                    if (rootComponent is ContainerControl)
                    {
                        EffectedForm = rootComponent as Form;
                    }
                }
            }
        }

        private void RefreshUI_Tick(object sender, EventArgs e)
        {
            try
            {
                Mainform_Shown(null, null);
                EffectedForm.Shown += Mainform_Shown;
                EffectedForm.Resize += Mainform_Resize;
                EffectedForm.LocationChanged += Mainform_Resize;
                RefreshUI.Enabled = false;
                RefreshUI.Dispose();
            }
            catch
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ds.Dispose();
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Change the shadow angle, sorry this option is trial and error")]
        public int ShadowAngle { get; set; } = 2;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The effected form(will remove ellipse from effected control)")]
        public Form EffectedForm { get; set; }

        private void Mainform_Shown(object sender, EventArgs e)
        {
            Rectangle bounds = EffectedForm.Bounds;
            ds.Bounds = bounds;
            ds.Location = new Point(EffectedForm.Location.X + ShadowAngle, EffectedForm.Location.Y + ShadowAngle);
            ds.Show();
            EffectedForm.BringToFront();
        }

        private void Mainform_Resize(object sender, EventArgs e)
        {
            ds.Visible = EffectedForm.WindowState == FormWindowState.Normal;
            if (ds.Visible)
            {
                Rectangle bounds = EffectedForm.Bounds;
                ds.Bounds = bounds;
                ds.Location = new Point(EffectedForm.Location.X + ShadowAngle, EffectedForm.Location.Y + ShadowAngle);
            }
            EffectedForm.BringToFront();
        }

        private readonly Timer RefreshUI = new();
        private readonly DropShadow ds = new();

        public class DropShadow : Form
        {
            public DropShadow()
            {
                BackColor = Color.Black;
                base.Opacity = 0.3;
                base.ShowInTaskbar = false;
                base.FormBorderStyle = FormBorderStyle.None;
                base.StartPosition = FormStartPosition.Manual;
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams createParams = base.CreateParams;
                    createParams.ExStyle = createParams.ExStyle | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
                    return createParams;
                }
            }

            private const int WS_EX_TRANSPARENT = 32;

            private const int WS_EX_NOACTIVATE = 134217728;
        }
    }

    #endregion
}