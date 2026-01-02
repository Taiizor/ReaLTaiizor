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
            get;
            set
            {
                field = value;
                CollapseChanged();
                CollapsedStateChanged();
                Invalidate();
            }
        } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The panel width expanded")]
        public int PanelWidthExpanded
        {
            get;
            set
            {
                field = value;
                if (!Collapsed)
                {
                    base.Size = new Size(field, base.Height);
                }
            }
        } = 200;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The panel width expanded")]
        public int PanelWidthCollapsed
        {
            get;
            set
            {
                field = value;
                if (Collapsed)
                {
                    base.Size = new Size(field, base.Height);
                }
            }
        } = 50;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hide controls when collapsed")]
        public bool HideControls { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The control used to collapse/expand the sliding panel")]
        public Control CollapseControl
        {
            get;
            set
            {
                field = value;
                if (field != null)
                {
                    field.Click += SwitchCollapsed;
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
            if (!Collapsed)
            {
                while (base.Width < PanelWidthExpanded)
                {
                    if (base.Width < PanelWidthExpanded / 10 * 6)
                    {
                        base.Size = new Size(base.Width + 30, base.Height);
                        sleeper.Sleep(40);
                    }
                    else if (base.Width < PanelWidthExpanded / 10 * 4)
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
                base.Size = new Size(PanelWidthExpanded, base.Height);
                if (HideControls)
                {
                    foreach (object obj in base.Controls)
                    {
                        Control control = (Control)obj;
                        if (control != CollapseControl)
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
                if (control2 != CollapseControl)
                {
                    control2.Visible = false;
                }
            }
            goto IL_FB;
        IL_5E:
            if (base.Width > PanelWidthExpanded / 5 * 3)
            {
                base.Size = new Size(base.Width - 30, base.Height);
                sleeper.Sleep(40);
            }
            else if (base.Width > PanelWidthExpanded / 5 * 2)
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
            if (base.Width <= PanelWidthCollapsed)
            {
                base.Size = new Size(PanelWidthCollapsed, base.Height);
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
    }

    #endregion
}