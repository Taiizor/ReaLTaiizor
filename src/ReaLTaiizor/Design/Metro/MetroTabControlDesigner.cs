#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
using System.Collections;
using ReaLTaiizor.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Child.Metro;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

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
					var addVerbs = new[]
					{
						new DesignerVerb("Add Tab", OnAddTab),
						new DesignerVerb("Remove Tab", OnRemoveTab)
					};

					_verbs = new DesignerVerbCollection();
					_verbs.AddRange(addVerbs);

					if (!(Control is MetroTabControl parentControl))
						return _verbs;
					switch (parentControl.TabPages.Count)
					{
						case 0:
							_verbs[1].Enabled = false;
							break;
						default:
							_verbs[1].Enabled = true;
							break;
					}
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
				_changeService.ComponentChanged += OnComponentChanged;
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
					m.Result = (IntPtr)User32._HTCLIENT;
			}
		}

		protected override bool GetHitTest(Point point)
		{
			var selectionService = (ISelectionService)GetService(typeof(ISelectionService));
			var selectedObject = selectionService?.PrimarySelection;
			if (selectedObject != null && selectedObject.Equals(Control))
			{
				var p = Control.PointToClient(point);

				var hti = new User32.TCHITTESTINFO(p, User32.TabControlHitTest.TCHT_ONITEM);

				var m = new Message
				{
					HWnd = Control.Handle,
					Msg = User32._TCM_HITTEST
				};

				var lParam = Marshal.AllocHGlobal(Marshal.SizeOf(hti));
				Marshal.StructureToPtr(hti, lParam, false);
				m.LParam = lParam;

				base.WndProc(ref m);
				Marshal.FreeHGlobal(lParam);

				if (m.Result.ToInt32() != -1)
					return hti.flags != User32.TabControlHitTest.TCHT_NOWHERE;
			}

			return false;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _changeService != null)
				_changeService.ComponentChanged -= OnComponentChanged;

			base.Dispose(disposing);
		}

		#endregion Override Methods

		#region Helper Methods

		private void OnAddTab(Object sender, EventArgs e)
		{
			var parentControl = Control as MetroTabControl;

			var oldTabs = parentControl?.TabPages;

			RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);
			var newTab = (MetroTabPage)_designerHost.CreateComponent(typeof(MetroTabPage));
			newTab.Text = newTab.Name;
			parentControl?.TabPages.Add(newTab);
			if (parentControl == null)
				return;
			parentControl.SelectedTab = newTab;
			RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl.TabPages);
		}

		private void OnRemoveTab(Object sender, EventArgs e)
		{
			var parentControl = Control as MetroTabControl;

			if (parentControl != null && parentControl.SelectedIndex < 0)
				return;

			var oldTabs = parentControl?.TabPages;

			RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["TabPages"]);
			_designerHost.DestroyComponent(parentControl?.SelectedTab);
			RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["TabPages"], oldTabs, parentControl?.TabPages);
		}

		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (!(e.Component is MetroTabControl parentControl) || e.Member.Name != "TabPages")
				return;
			foreach (DesignerVerb verb in Verbs)
			{
				if (verb.Text != "Remove Tab")
					continue;
				switch (parentControl.TabPages.Count)
				{
					case 0:
						verb.Enabled = false;
						break;
					default:
						verb.Enabled = true;
						break;
				}

				break;
			}
		}

		#endregion Helper Methods
	}

	#endregion
}