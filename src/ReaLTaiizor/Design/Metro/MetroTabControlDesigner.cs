#region Imports

using ReaLTaiizor.Child.Metro;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Native;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroTabControlDesignerDesign

    public class MetroTabControlDesigner : ParentControlDesigner
    {
        #region Instance Members

        private DesignerVerbCollection _verbs;
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;

        #endregion Instance Members

        #region Constructor

        #endregion Constructor

        #region Property

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_verbs == null)
                {
                    DesignerVerb[] addVerbs = new[]
                    {
                        new DesignerVerb("Add Tab", OnAddTab),
                        new DesignerVerb("Remove Tab", OnRemoveTab)
                    };

                    _verbs = new();
                    _verbs.AddRange(addVerbs);

                    if (Control is not MetroTabControl parentControl)
                    {
                        return _verbs;
                    }

                    _verbs[1].Enabled = parentControl.TabPages.Count switch
                    {
                        0 => false,
                        _ => true,
                    };
                }

                return _verbs;
            }
        }

        #endregion Property

        #region Override Methods

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            if (_changeService != null)
            {
                _changeService.ComponentChanged += OnComponentChanged;
            }
        }

        protected override void PostFilterProperties(IDictionary properties)
        {
            properties.Remove("Margin");
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("Enabled");
            properties.Remove("RightToLeft");
            properties.Remove("RightToLeftLayout");
            properties.Remove("ApplicationSettings");
            properties.Remove("DataBindings");

            base.PostFilterProperties(properties);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == (int)User32.Msgs.WM_NCHITTEST)
            {
                if (m.Result.ToInt32() == User32._HT_TRANSPARENT)
                {
                    m.Result = (IntPtr)User32._HTCLIENT;
                }
            }
        }

        protected override bool GetHitTest(Point point)
        {
            ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
            object selectedObject = selectionService?.PrimarySelection;
            if (selectedObject != null && selectedObject.Equals(Control))
            {
                Point p = Control.PointToClient(point);

                User32.TCHITTESTINFO hti = new(p, User32.TabControlHitTest.TCHT_ONITEM);

                Message m = new()
                {
                    HWnd = Control.Handle,
                    Msg = User32._TCM_HITTEST
                };

                IntPtr lParam = Marshal.AllocHGlobal(Marshal.SizeOf(hti));
                Marshal.StructureToPtr(hti, lParam, false);
                m.LParam = lParam;

                base.WndProc(ref m);
                Marshal.FreeHGlobal(lParam);

                if (m.Result.ToInt32() != -1)
                {
                    return hti.flags != User32.TabControlHitTest.TCHT_NOWHERE;
                }
            }

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _changeService != null)
            {
                _changeService.ComponentChanged -= OnComponentChanged;
            }

            base.Dispose(disposing);
        }

        #endregion Override Methods

        #region Helper Methods

        private void OnAddTab(object sender, EventArgs e)
        {
            MetroTabControl parentControl = Control as MetroTabControl;

            TabControl.TabPageCollection oldTabs = parentControl?.TabPages;

            RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);
            MetroTabPage newTab = (MetroTabPage)_designerHost.CreateComponent(typeof(MetroTabPage));
            newTab.Text = newTab.Name;
            parentControl?.TabPages.Add(newTab);
            if (parentControl == null)
            {
                return;
            }

            parentControl.SelectedTab = newTab;
            RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl.TabPages);
        }

        private void OnRemoveTab(object sender, EventArgs e)
        {
            MetroTabControl parentControl = Control as MetroTabControl;

            if (parentControl != null && parentControl.SelectedIndex < 0)
            {
                return;
            }

            TabControl.TabPageCollection oldTabs = parentControl?.TabPages;

            RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);
            _designerHost.DestroyComponent(parentControl?.SelectedTab);
            RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl?.TabPages);
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (e.Component is not MetroTabControl parentControl || e.Member.Name != "TabPages")
            {
                return;
            }

            foreach (DesignerVerb verb in Verbs)
            {
                if (verb.Text != "Remove Tab")
                {
                    continue;
                }

                verb.Enabled = parentControl.TabPages.Count switch
                {
                    0 => false,
                    _ => true,
                };
                break;
            }
        }

        #endregion Helper Methods
    }

    #endregion
}