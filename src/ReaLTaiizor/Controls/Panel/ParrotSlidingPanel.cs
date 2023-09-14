#region Imports

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotSlidingPanel

    public class ParrotSlidingPanel : ParrotGradientPanel
    {
        public ParrotSlidingPanel()
        {
            Dock = DockStyle.Left;
            CollapseChanged();
            base.BottomRight = Color.DodgerBlue;
            base.TopLeft = Color.Black;
            base.TopRight = Color.Black;
            base.BottomLeft = Color.Black;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the panel collapsed")]
        public bool Collapsed
        {
            get => collapsed;
            set
            {
                collapsed = value;
                CollapseChanged();
                CollapsedStateChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The panel width expanded")]
        public int PanelWidthExpanded
        {
            get => panelWidthExpanded;
            set
            {
                panelWidthExpanded = value;
                if (!Collapsed)
                {
                    base.Size = new Size(panelWidthExpanded, base.Height);
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The panel width expanded")]
        public int PanelWidthCollapsed
        {
            get => panelWidthCollapsed;
            set
            {
                panelWidthCollapsed = value;
                if (Collapsed)
                {
                    base.Size = new Size(panelWidthCollapsed, base.Height);
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hide controls when collapsed")]
        public bool HideControls { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The control used to collapse/expand the sliding panel")]
        public Control CollapseControl
        {
            get => collapseControl;
            set
            {
                collapseControl = value;
                if (collapseControl != null)
                {
                    collapseControl.Click += SwitchCollapsed;
                }
            }
        }

        private void SwitchCollapsed(object sender, EventArgs e)
        {
            if (Collapsed)
            {
                Collapsed = false;
                return;
            }
            Collapsed = true;
        }

        private void CollapseChanged()
        {
            if (!collapsed)
            {
                while (base.Width < panelWidthExpanded)
                {
                    if (base.Width < panelWidthExpanded / 10 * 6)
                    {
                        base.Size = new Size(base.Width + 30, base.Height);
                        sleeper.Sleep(40);
                    }
                    else if (base.Width < panelWidthExpanded / 10 * 4)
                    {
                        base.Size = new Size(base.Width + 20, base.Height);
                        sleeper.Sleep(40);
                    }
                    else
                    {
                        base.Size = new Size(base.Width + 10, base.Height);
                        sleeper.Sleep(40);
                    }
                }
                base.Size = new Size(panelWidthExpanded, base.Height);
                if (HideControls)
                {
                    foreach (object obj in base.Controls)
                    {
                        Control control = (Control)obj;
                        if (control != collapseControl)
                        {
                            control.Visible = true;
                        }
                    }
                }
                return;
            }
            if (!HideControls)
            {
                goto IL_FB;
            }
            IEnumerator enumerator = base.Controls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object obj2 = enumerator.Current;
                Control control2 = (Control)obj2;
                if (control2 != collapseControl)
                {
                    control2.Visible = false;
                }
            }
            goto IL_FB;
        IL_5E:
            if (base.Width > panelWidthExpanded / 5 * 3)
            {
                base.Size = new Size(base.Width - 30, base.Height);
                sleeper.Sleep(40);
            }
            else if (base.Width > panelWidthExpanded / 5 * 2)
            {
                base.Size = new Size(base.Width - 20, base.Height);
                sleeper.Sleep(40);
            }
            else
            {
                base.Size = new Size(base.Width - 10, base.Height);
                sleeper.Sleep(40);
            }
        IL_FB:
            if (base.Width <= panelWidthCollapsed)
            {
                base.Size = new Size(panelWidthCollapsed, base.Height);
                return;
            }
            goto IL_5E;
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            if (Dock != DockStyle.Left & Dock != DockStyle.Right)
            {
                Dock = DockStyle.Left;
            }
        }

        public event EventHandler OnCollapsedStateChanged;

        protected virtual void CollapsedStateChanged()
        {
            OnCollapsedStateChanged?.Invoke(this, new EventArgs());
        }

        private readonly ParrotSleeper sleeper = new();

        private bool collapsed = true;

        private int panelWidthExpanded = 200;

        private int panelWidthCollapsed = 50;
        private Control collapseControl;
    }

    #endregion
}