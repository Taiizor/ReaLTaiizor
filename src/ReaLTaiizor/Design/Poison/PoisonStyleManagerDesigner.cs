#region Imports

using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Design.Poison
{
    #region PoisonStyleManagerDesignerDesign

    internal class PoisonStyleManagerDesigner : ComponentDesigner
    {
        private DesignerVerbCollection designerVerbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (designerVerbs != null)
                {
                    return designerVerbs;
                }

                designerVerbs = new DesignerVerbCollection
                {
                    new DesignerVerb("Reset Styles to Default", OnResetStyles)
                };

                return designerVerbs;
            }
        }

        private IDesignerHost designerHost;
        public IDesignerHost DesignerHost
        {
            get
            {
                if (designerHost != null)
                {
                    return designerHost;
                }

                designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));

                return designerHost;
            }
        }

        private IComponentChangeService componentChangeService;
        public IComponentChangeService ComponentChangeService
        {
            get
            {
                if (componentChangeService != null)
                {
                    return componentChangeService;
                }

                componentChangeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

                return componentChangeService;
            }
        }

        private void OnResetStyles(object sender, EventArgs args)
        {
            PoisonStyleManager styleManager = Component as PoisonStyleManager;
            if (styleManager != null)
            {
                if (styleManager.Owner == null)
                {
                    MessageBox.Show("StyleManager needs the Owner property assigned to before it can reset styles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ResetStyles(styleManager, styleManager.Owner);
        }

        private void ResetStyles(PoisonStyleManager styleManager, Control control)
        {
            if (control is IPoisonForm container && !ReferenceEquals(styleManager, container.StyleManager))
            {
                return;
            }

            if (control is IPoisonControl)
            {
                ResetProperty(control, "Style", ColorStyle.Default);
                ResetProperty(control, "Theme", ThemeStyle.Default);
            }
            else if (control is IPoisonComponent)
            {
                ResetProperty(control, "Style", ColorStyle.Default);
                ResetProperty(control, "Theme", ThemeStyle.Default);
            }

            if (control.ContextMenuStrip != null)
            {
                ResetStyles(styleManager, control.ContextMenuStrip);
            }

            if (control is TabControl tabControl)
            {
                foreach (TabPage tp in tabControl.TabPages)
                {
                    ResetStyles(styleManager, tp);
                }
            }

            if (control.Controls != null)
            {
                foreach (Control child in control.Controls)
                {
                    ResetStyles(styleManager, child);
                }
            }
        }

        private void ResetProperty(Control control, string name, object newValue)
        {
            PropertyDescriptor typeDescriptor = TypeDescriptor.GetProperties(control)[name];
            if (typeDescriptor == null)
            {
                return;
            }

            object oldValue = typeDescriptor.GetValue(control);

            if (newValue.Equals(oldValue))
            {
                return;
            }

            ComponentChangeService.OnComponentChanging(control, typeDescriptor);
            typeDescriptor.SetValue(control, newValue);
            ComponentChangeService.OnComponentChanged(control, typeDescriptor, oldValue, newValue);
        }
    }

    #endregion
}