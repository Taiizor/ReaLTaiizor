#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialContextMenuStrip

    [ToolboxItem(false)]
    public class MaterialContextMenuStrip : ContextMenuStrip, MaterialControlI
    {
        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        internal AnimationManager AnimationManager;

        internal Point AnimationSource;

        public delegate void ItemClickStart(object sender, ToolStripItemClickedEventArgs e);

        public event ItemClickStart OnItemClickStart;

        public MaterialContextMenuStrip()
        {
            Renderer = new MaterialToolStripRender();

            AnimationManager = new AnimationManager(false)
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };
            AnimationManager.OnAnimationProgress += sender => Invalidate();
            AnimationManager.OnAnimationFinished += sender => OnItemClicked(_delayesArgs);

            BackColor = SkinManager.BackdropColor;
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            base.OnMouseUp(mea);

            AnimationSource = mea.Location;
        }

        private ToolStripItemClickedEventArgs _delayesArgs;

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is not null and not ToolStripSeparator)
            {
                if (e == _delayesArgs)
                {
                    //The event has been fired manualy because the args are the ones we saved for delay
                    base.OnItemClicked(e);
                }
                else
                {
                    //Interrupt the default on click, saving the args for the delay which is needed to display the animaton
                    _delayesArgs = e;

                    //Fire custom event to trigger actions directly but keep cms open
                    OnItemClickStart?.Invoke(this, e);

                    //Start animation
                    AnimationManager.StartNewAnimation(AnimationDirection.In);
                }
            }
        }
    }

    public class MaterialToolStripMenuItem : ToolStripMenuItem
    {
        public MaterialToolStripMenuItem()
        {
            AutoSize = false;
            Size = new Size(128, 32);
        }

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            ToolStripDropDown baseDropDown = base.CreateDefaultDropDown();
            if (DesignMode)
            {
                return baseDropDown;
            }

            MaterialContextMenuStrip defaultDropDown = new();
            defaultDropDown.Items.AddRange(baseDropDown.Items);

            return defaultDropDown;
        }
    }

    internal class MaterialToolStripRender : ToolStripProfessionalRenderer, MaterialControlI
    {
        private const int LEFT_PADDING = 16;
        private const int RIGHT_PADDING = 8;

        //Properties for managing the material design properties
        public int Depth { get; set; }

        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        public MaterialMouseState MouseState { get; set; }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle itemRect = GetItemRect(e.Item);
            Rectangle textRect = new(LEFT_PADDING, itemRect.Y, itemRect.Width - (LEFT_PADDING + RIGHT_PADDING), itemRect.Height);

            using MaterialNativeTextRenderer NativeText = new(g);
            NativeText.DrawTransparentText(e.Text, SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Body2),
                e.Item.Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                textRect.Location,
                textRect.Size,
                MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(SkinManager.BackgroundColor);

            //Draw background
            Rectangle itemRect = GetItemRect(e.Item);
            g.FillRectangle(e.Item.Selected && e.Item.Enabled ? SkinManager.BackgroundFocusBrush : SkinManager.BackgroundBrush, itemRect);

            //Ripple animation
            if (e.ToolStrip is MaterialContextMenuStrip toolStrip)
            {
                AnimationManager animationManager = toolStrip.AnimationManager;
                Point animationSource = toolStrip.AnimationSource;
                if (toolStrip.AnimationManager.IsAnimating() && e.Item.Bounds.Contains(animationSource))
                {
                    for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                    {
                        double animationValue = animationManager.GetProgress(i);
                        SolidBrush rippleBrush = new(Color.FromArgb((int)(51 - (animationValue * 50)), Color.Black));
                        int rippleSize = (int)(animationValue * itemRect.Width * 2.5);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - (rippleSize / 2), itemRect.Y - itemRect.Height, rippleSize, itemRect.Height * 3));
                    }
                }
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(SkinManager.BackgroundBrush, e.Item.Bounds);
            g.DrawLine(
                new Pen(SkinManager.DividersColor),
                new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2),
                new Point(e.Item.Bounds.Right, e.Item.Bounds.Height / 2));
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            e.ToolStrip.BackColor = SkinManager.BackgroundColor;
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            const int ARROW_SIZE = 4;

            Point arrowMiddle = new(e.ArrowRectangle.X + (e.ArrowRectangle.Width / 2), e.ArrowRectangle.Y + (e.ArrowRectangle.Height / 2));
            Brush arrowBrush = e.Item.Enabled ? SkinManager.TextHighEmphasisBrush : SkinManager.TextDisabledOrHintBrush;
            using GraphicsPath arrowPath = new();
            arrowPath.AddLines(
                new[] {
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y - ARROW_SIZE),
                        new Point(arrowMiddle.X, arrowMiddle.Y),
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y + ARROW_SIZE) });
            arrowPath.CloseFigure();

            g.FillPath(arrowBrush, arrowPath);
        }

        private Rectangle GetItemRect(ToolStripItem item)
        {
            return new Rectangle(0, item.ContentRectangle.Y, item.ContentRectangle.Width, item.ContentRectangle.Height);
        }
    }

    #endregion
}