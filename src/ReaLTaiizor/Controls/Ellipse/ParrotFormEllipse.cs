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
    #region ParrotFormEllipse

    public class ParrotFormEllipse : Component
    {
        public ParrotFormEllipse()
        {
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
            if (effectedForm != null)
            {
                effectedForm.FormBorderStyle = FormBorderStyle.None;

                effectedForm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedForm.Width, effectedForm.Height, cornerRadius, cornerRadius));
                effectedForm.SizeChanged += Container_SizeChanged;
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

            if (effectedForm != null)
            {
                effectedForm.FormBorderStyle = DefaultStyle;

                effectedForm.Region = DefaultFormRegion;
            }
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
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
                if (value != effectedForm)
                {
                    effectedForm = value;
                    UpdateForm();
                }
            }
        }

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private Form DefaultForm = null;

        private FormBorderStyle DefaultStyle;

        private Region DefaultFormRegion = null;

        private int cornerRadius = 10;

        private Form effectedForm = null;
    }

    #endregion
}