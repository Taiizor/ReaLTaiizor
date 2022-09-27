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
    #region ParrotObjectEllipse

    public class ParrotObjectEllipse : Component
    {
        public ParrotObjectEllipse()
        {
            UpdateControl();
            UpdateForm();
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

                    if (rootComponent is Form)
                    {
                        effectedForm = rootComponent as Form;

                        DefaultForm = rootComponent as Form;

                        if (DefaultForm != null)
                        {
                            DefaultFormRegion = DefaultForm.Region;
                            DefaultStyle = DefaultForm.FormBorderStyle;
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

            if (effectedForm != null)
            {
                effectedForm.FormBorderStyle = FormBorderStyle.None;

                effectedForm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedForm.Width, effectedForm.Height, cornerRadius, cornerRadius));
                effectedForm.SizeChanged += Container_SizeChanged;
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

        private void UpdateForm()
        {
            if (DefaultForm != null)
            {
                DefaultForm.FormBorderStyle = DefaultStyle;

                DefaultForm.Region = DefaultFormRegion;
            }

            if (effectedForm != null)
            {
                DefaultForm = effectedForm;

                DefaultFormRegion = effectedForm.Region;

                DefaultStyle = effectedForm.FormBorderStyle;

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

            if (effectedForm != null)
            {
                effectedForm.FormBorderStyle = DefaultStyle;

                effectedForm.Region = DefaultFormRegion;
            }
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
            if (effectedControl != null)
            {
                effectedControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedControl.Width, effectedControl.Height, cornerRadius, cornerRadius));
            }

            if (effectedForm != null)
            {
                effectedForm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedForm.Width, effectedForm.Height, cornerRadius, cornerRadius));
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
                if (value != EffectedForm || value == null)
                {
                    effectedControl = value;
                    UpdateControl();
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The effected form(will remove ellipse from effected control)")]
        public Form EffectedForm
        {
            get
            {
                if (effectedForm != null)
                {
                    return effectedForm;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != EffectedControl || value == null)
                {
                    effectedForm = value;
                    UpdateForm();
                }
            }
        }

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private Control DefaultControl = null;

        private Form DefaultForm = null;

        private FormBorderStyle DefaultStyle;

        private Region DefaultControlRegion = null;

        private Region DefaultFormRegion = null;

        private int cornerRadius = 10;

        private Control effectedControl = null;

        private Form effectedForm = null;
    }

    #endregion
}