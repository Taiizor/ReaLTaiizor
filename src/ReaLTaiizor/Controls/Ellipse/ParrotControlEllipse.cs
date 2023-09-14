#region Imports

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotControlEllipse

    public class ParrotControlEllipse : Component
    {
        public ParrotControlEllipse()
        {
            UpdateControl();
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

                    if (rootComponent is ContainerControl and not Form)
                    {
                        effectedControl = rootComponent as ContainerControl;

                        DefaultControl = rootComponent as ContainerControl;

                        if (DefaultControl != null)
                        {
                            DefaultControlRegion = DefaultControl.Region;
                        }
                    }
                }
            }
        }

        private void SetCustomRegion()
        {
            if (effectedControl != null)
            {
                effectedControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedControl.Width, effectedControl.Height, cornerRadius, cornerRadius));
                effectedControl.SizeChanged += Container_SizeChanged;
            }
        }

        private void UpdateControl()
        {
            if (DefaultControl != null)
            {
                DefaultControl.Region = DefaultControlRegion;
            }

            if (effectedControl != null)
            {
                DefaultControl = effectedControl;

                DefaultControlRegion = effectedControl.Region;

                SetCustomRegion();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (effectedControl != null)
            {
                effectedControl.Region = DefaultControlRegion;
            }
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
            if (effectedControl != null)
            {
                effectedControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedControl.Width, effectedControl.Height, cornerRadius, cornerRadius));
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The corner radius")]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = value;
                SetCustomRegion();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The effected control")]
        public Control EffectedControl
        {
            get
            {
                if (effectedControl != null)
                {
                    return effectedControl;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != effectedControl && value is not Form)
                {
                    effectedControl = value;
                    UpdateControl();
                }
            }
        }

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private Control DefaultControl = null;

        private Region DefaultControlRegion = null;

        private int cornerRadius = 10;

        private Control effectedControl = null;
    }

    #endregion
}