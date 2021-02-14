#region Imports

using ReaLTaiizor.Enum.Crown;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDockContentDocking

    [ToolboxItem(false)]
    public class CrownDockContent : UserControl
    {
        #region Event Handler Region

        public event EventHandler DockTextChanged;

        #endregion

        #region Field Region

        private string _dockText;
        private Image _icon;

        #endregion

        #region Property Region

        [Category("Appearance")]
        [Description("Determines the text that will appear in the content tabs and headers.")]
        public string DockText
        {
            get => _dockText;
            set
            {
                string oldText = _dockText;

                _dockText = value;

                DockTextChanged?.Invoke(this, null);

                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the icon that will appear in the content tabs and headers.")]
        public Image Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                Invalidate();
            }
        }

        [Category("Layout")]
        [Description("Determines the default area of the dock panel this content will be added to.")]
        [DefaultValue(Enum.Crown.DockArea.Document)]
        public DockArea DefaultDockArea { get; set; }

        [Category("Behavior")]
        [Description("Determines the key used by this content in the dock serialization.")]
        public string SerializationKey { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockPanel DockPanel { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockRegion DockRegion { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockGroup DockGroup { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockArea DockArea { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Order { get; set; }

        #endregion

        #region Constructor Region

        public CrownDockContent()
        {
            BackColor = Color.Transparent; //ThemeProvider.Theme.Colors.GreyBackground;
        }

        #endregion

        #region Method Region

        public virtual void Close()
        {
            if (DockPanel != null)
            {
                DockPanel.RemoveContent(this);
            }
        }

        #endregion

        #region Event Handler Region

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (DockPanel == null)
            {
                return;
            }

            DockPanel.ActiveContent = this;
        }

        #endregion
    }

    #endregion
}