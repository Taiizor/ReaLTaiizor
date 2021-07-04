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
                IDesignerHost designerHost = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (designerHost != null)
                {
                    IComponent rootComponent = designerHost.RootComponent;
                    if (rootComponent is ContainerControl)
                    {
                        effectedControl = rootComponent as ContainerControl;
                        DefaultControl = rootComponent as ContainerControl;
                        DefaultRegion = DefaultControl.Region;
                        try
                        {
                            DefaultStyle = ((Form)DefaultControl).FormBorderStyle;
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void SetCustomRegion()
        {
            if (effectedControl != null)
            {
                try
                {
                    ((Form)effectedControl).FormBorderStyle = FormBorderStyle.None;
                }
                catch
                {
                }
                effectedControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedControl.Width, effectedControl.Height, cornerRadius, cornerRadius));
                effectedControl.SizeChanged += Container_SizeChanged;
            }
        }

        private void RefreshUI_Tick(object sender, EventArgs e)
        {
            UpdateControl();
            RefreshUI.Enabled = false;
            RefreshUI.Dispose();
        }

        private void UpdateControl()
        {
            if (DefaultControl != null)
            {
                try
                {
                    ((Form)DefaultControl).FormBorderStyle = DefaultStyle;
                }
                catch
                {
                }
                DefaultControl.Region = DefaultRegion;
            }
            if (effectedControl != null)
            {
                try
                {
                    DefaultControl = (Form)effectedControl;
                }
                catch
                {
                    DefaultControl = effectedControl;
                }
                DefaultRegion = effectedControl.Region;
                try
                {
                    DefaultStyle = ((Form)effectedControl).FormBorderStyle;
                }
                catch
                {
                }
                SetCustomRegion();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (effectedControl != null)
            {
                try
                {
                    ((Form)effectedControl).FormBorderStyle = DefaultStyle;
                }
                catch
                {
                }
                effectedControl.Region = DefaultRegion;
            }
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
            effectedControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, effectedControl.Width, effectedControl.Height, cornerRadius, cornerRadius));
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
            get => effectedControl;
            set
            {
                effectedControl = value;
                UpdateControl();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The effected form(will remove ellipse from effected control)")]
        public Form EffectedForm
        {
            get
            {
                Form result;
                try
                {
                    result = effectedControl as Form;
                }
                catch
                {
                    result = null;
                }
                return result;
            }
            set
            {
                effectedControl = value;
                UpdateControl();
            }
        }

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private readonly Timer RefreshUI = new();

        private Control DefaultControl;

        private FormBorderStyle DefaultStyle;

        private Region DefaultRegion;

        private int cornerRadius = 10;

        private Control effectedControl;
    }

    #endregion
}