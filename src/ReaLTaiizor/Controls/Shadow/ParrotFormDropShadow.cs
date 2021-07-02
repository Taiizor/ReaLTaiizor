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
            RefreshUI.Interval = 100;
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
                        effectedForm = rootComponent as Form;
                    }
                }
            }
        }

        private void RefreshUI_Tick(object sender, EventArgs e)
        {
            try
            {
                Mainform_Shown(null, null);
                effectedForm.Shown += Mainform_Shown;
                effectedForm.Resize += Mainform_Resize;
                effectedForm.LocationChanged += Mainform_Resize;
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
        public int ShadowAngle
        {
            get => shadowAngle;
            set => shadowAngle = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The effected form(will remove ellipse from effected control)")]
        public Form EffectedForm
        {
            get => effectedForm;
            set => effectedForm = value;
        }

        private void Mainform_Shown(object sender, EventArgs e)
        {
            Rectangle bounds = effectedForm.Bounds;
            ds.Bounds = bounds;
            ds.Location = new Point(effectedForm.Location.X + shadowAngle, effectedForm.Location.Y + shadowAngle);
            ds.Show();
            effectedForm.BringToFront();
        }

        private void Mainform_Resize(object sender, EventArgs e)
        {
            ds.Visible = effectedForm.WindowState == FormWindowState.Normal;
            if (ds.Visible)
            {
                Rectangle bounds = effectedForm.Bounds;
                ds.Bounds = bounds;
                ds.Location = new Point(effectedForm.Location.X + shadowAngle, effectedForm.Location.Y + shadowAngle);
            }
            effectedForm.BringToFront();
        }

        private readonly Timer RefreshUI = new();

        private int shadowAngle = 2;

        private Form effectedForm;

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