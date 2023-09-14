#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Native;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Poison
{
    #region PoisonTabControlDesignerDesign

    internal class PoisonTabControlDesigner : ParentControlDesigner
    {
        #region Fields

        private readonly DesignerVerbCollection designerVerbs = new();

        private IDesignerHost designerHost;

        private ISelectionService selectionService;

        public override SelectionRules SelectionRules => Control.Dock == DockStyle.Fill ? SelectionRules.Visible : base.SelectionRules;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (designerVerbs.Count == 2)
                {
                    PoisonTabControl myControl = (PoisonTabControl)Control;
                    designerVerbs[1].Enabled = myControl.TabCount != 0;
                }
                return designerVerbs;
            }
        }

        public IDesignerHost DesignerHost => designerHost ??= (IDesignerHost)GetService(typeof(IDesignerHost));

        public ISelectionService SelectionService => selectionService ??= (ISelectionService)GetService(typeof(ISelectionService));

        #endregion

        #region Constructor

        public PoisonTabControlDesigner()
        {
            DesignerVerb verb1 = new("Add Tab", OnAddPage);
            DesignerVerb verb2 = new("Remove Tab", OnRemovePage);
            designerVerbs.AddRange
            (
                new[]
                {
                    verb1,
                    verb2
                }
            );
        }

        #endregion

        #region Private Methods

        private void OnAddPage(object sender, EventArgs e)
        {
            PoisonTabControl parentControl = (PoisonTabControl)Control;
            Control.ControlCollection oldTabs = parentControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);

            PoisonTabPage p = (PoisonTabPage)DesignerHost.CreateComponent(typeof(PoisonTabPage));
            p.Text = p.Name;
            parentControl.TabPages.Add(p);

            RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl.TabPages);
            parentControl.SelectedTab = p;

            SetVerbs();
        }

        private void OnRemovePage(object sender, EventArgs e)
        {
            PoisonTabControl parentControl = (PoisonTabControl)Control;
            Control.ControlCollection oldTabs = parentControl.Controls;

            if (parentControl.SelectedIndex < 0)
            {
                return;
            }

            RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);

            DesignerHost.DestroyComponent(parentControl.TabPages[parentControl.SelectedIndex]);

            RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl.TabPages);

            SelectionService.SetSelectedComponents
            (
                new IComponent[]
                {
                    parentControl
                },
                SelectionTypes.Auto
            );

            SetVerbs();
        }

        private void SetVerbs()
        {
            PoisonTabControl parentControl = (PoisonTabControl)Control;

            Verbs[1].Enabled = parentControl.TabPages.Count switch
            {
                0 => false,
                _ => true,
            };
        }

        #endregion

        #region Overrides

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case (int)WinApi.Messages.WM_NCHITTEST:
                    if (m.Result.ToInt32() == (int)WinApi.HitTest.HTTRANSPARENT)
                    {
                        m.Result = (IntPtr)WinApi.HitTest.HTCLIENT;
                    }

                    break;
            }
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            if (SelectionService.PrimarySelection == Control)
            {
                WinApi.TCHITTESTINFO hti = new()
                {
                    pt = Control.PointToClient(point),
                    flags = 0
                };

                Message m = new()
                {
                    HWnd = Control.Handle,
                    Msg = WinApi.TCM_HITTEST
                };

                IntPtr lparam =
                    System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(hti));
                System.Runtime.InteropServices.Marshal.StructureToPtr(hti, lparam, false);
                m.LParam = lparam;

                base.WndProc(ref m);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(lparam);

                if (m.Result.ToInt32() != -1)
                {
                    return hti.flags != (int)WinApi.TabControlHitTest.TCHT_NOWHERE;
                }
            }

            return false;
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("AutoEllipsis");
            properties.Remove("UseCompatibleTextRendering");

            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");

            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("UseVisualStyleBackColor");

            properties.Remove("Font");
            properties.Remove("RightToLeft");

            base.PreFilterProperties(properties);
        }
        #endregion
    }

    #endregion
}