#region Imports

using System;
using ReaLTaiizor.Util;
using System.Reflection;
using ReaLTaiizor.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Interface.Poison;

#endregion

namespace ReaLTaiizor.Manager
{
    #region PoisonStyleManagerManager

    [Designer(typeof(PoisonStyleManagerDesigner))]
    public sealed class PoisonStyleManager : Component, ICloneable, ISupportInitialize
    {
        #region Fields

        private readonly IContainer parentContainer;

        private ColorStyle poisonStyle = PoisonDefaults.Style;
        [DefaultValue(PoisonDefaults.Style)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ColorStyle Style
        {
            get => poisonStyle;
            set
            {
                if (value == ColorStyle.Default)
                    value = PoisonDefaults.Style;

                poisonStyle = value;

                if (!isInitializing)
                    Update();
            }
        }

        private ThemeStyle poisonTheme = PoisonDefaults.Theme;
        [DefaultValue(PoisonDefaults.Theme)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ThemeStyle Theme
        {
            get => poisonTheme;
            set
            {
                if (value == ThemeStyle.Default)
                    value = PoisonDefaults.Theme;

                poisonTheme = value;

                if (!isInitializing)
                    Update();
            }
        }

        private ContainerControl owner;
        public ContainerControl Owner
        {
            get => owner;
            set
            {
                if (owner != null)
                    owner.ControlAdded -= ControlAdded;

                owner = value;

                if (value != null)
                {
                    owner.ControlAdded += ControlAdded;

                    if (!isInitializing)
                        UpdateControl(value);
                }
            }
        }

        #endregion

        #region Constructor

        public PoisonStyleManager()
        {

        }

        public PoisonStyleManager(IContainer parentContainerr) : this()
        {
            if (parentContainerr != null)
            {
                parentContainer = parentContainerr;
                parentContainer.Add(this);
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            PoisonStyleManager newStyleManager = new PoisonStyleManager
            {
                poisonTheme = Theme,
                poisonStyle = Style
            };
            return newStyleManager;
        }

        public object Clone(ContainerControl owner)
        {
            PoisonStyleManager clonedManager = Clone() as PoisonStyleManager;

            if (owner is IPoisonForm)
            {
                clonedManager.Owner = owner;
                ((IPoisonForm)owner).StyleManager = clonedManager;

                Type parentForm = owner.GetType();
                FieldInfo fieldInfo = parentForm.GetField("components",
                BindingFlags.Instance |
                     BindingFlags.NonPublic);

                if (fieldInfo == null) return clonedManager;

                IContainer mother = (IContainer)fieldInfo.GetValue(owner);
                if (mother == null) return clonedManager;

                // Check for a helper component
                foreach (Component obj in mother.Components)
                {
                    if (obj is IPoisonComponent)
                        ApplyTheme((IPoisonComponent)obj);

                    if (obj.GetType() == typeof(PoisonContextMenuStrip))
                        ApplyTheme((PoisonContextMenuStrip)obj);
                }
            }

            return clonedManager;
        }

        #endregion

        #region ISupportInitialize

        private bool isInitializing;

        void ISupportInitialize.BeginInit()
        {
            isInitializing = true;
        }

        void ISupportInitialize.EndInit()
        {
            isInitializing = false;
            Update();
        }

        #endregion

        #region Management Methods

        private void ControlAdded(object sender, ControlEventArgs e)
        {
            if (!isInitializing)
                UpdateControl(e.Control);
        }

        public void Update()
        {
            if (owner != null)
                UpdateControl(owner);

            if (parentContainer == null || parentContainer.Components == null)
                return;

            foreach (Object obj in parentContainer.Components)
            {
                if (obj is IPoisonComponent)
                    ApplyTheme((IPoisonComponent)obj);

                if (obj.GetType() == typeof(PoisonContextMenuStrip))
                    ApplyTheme((PoisonContextMenuStrip)obj);
            }
        }

        private void UpdateControl(Control ctrl)
        {
            if (ctrl == null)
                return;

            IPoisonControl poisonControl = ctrl as IPoisonControl;
            if (poisonControl != null)
                ApplyTheme(poisonControl);

            IPoisonComponent poisonComponent = ctrl as IPoisonComponent;
            if (poisonComponent != null)
                ApplyTheme(poisonComponent);

            TabControl tabControl = ctrl as TabControl;
            if (tabControl != null)
            {
                foreach (System.Windows.Forms.TabPage tp in ((TabControl)ctrl).TabPages)
                    UpdateControl(tp);
            }

            if (ctrl.Controls != null)
            {
                foreach (Control child in ctrl.Controls)
                    UpdateControl(child);
            }

            if (ctrl.ContextMenuStrip != null)
                UpdateControl(ctrl.ContextMenuStrip);

            ctrl.Refresh();
        }

        private void ApplyTheme(IPoisonControl control)
        {
            control.StyleManager = this;
        }

        private void ApplyTheme(IPoisonComponent component)
        {
            component.StyleManager = this;
        }

        #endregion
    }

    #endregion
}