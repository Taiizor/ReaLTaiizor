#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Action.Metro
{
    #region MetroToolTipActionListAction

    internal class MetroToolTipActionList : DesignerActionList
    {
        private readonly MetroToolTip _metroToolTip;

        public MetroToolTipActionList(IComponent component) : base(component)
        {
            _metroToolTip = (MetroToolTip)component;
        }

        public Style Style
        {
            get => _metroToolTip.Style;
            set => _metroToolTip.Style = value;
        }

        public string ThemeAuthor => _metroToolTip.ThemeAuthor;

        public string ThemeName => _metroToolTip.ThemeName;

        public MetroStyleManager StyleManager
        {
            get => _metroToolTip.StyleManager;
            set => _metroToolTip.StyleManager = value;
        }

        public bool Active
        {
            get => _metroToolTip.Active;
            set => _metroToolTip.Active = value;
        }

        public int AutomaticDelay
        {
            get => _metroToolTip.AutomaticDelay;
            set => _metroToolTip.AutomaticDelay = value;
        }

        public int AutoPopDelay
        {
            get => _metroToolTip.AutoPopDelay;
            set => _metroToolTip.AutoPopDelay = value;
        }

        public int InitialDelay
        {
            get => _metroToolTip.InitialDelay;
            set => _metroToolTip.InitialDelay = value;
        }

        public bool StripAmpersands
        {
            get => _metroToolTip.StripAmpersands;
            set => _metroToolTip.StripAmpersands = value;
        }

        public bool UseAnimation
        {
            get => _metroToolTip.UseAnimation;
            set => _metroToolTip.UseAnimation = value;
        }

        public bool UseFading
        {
            get => _metroToolTip.UseFading;
            set => _metroToolTip.UseFading = value;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new()
            {
                new DesignerActionHeaderItem("Metro"),
                new DesignerActionPropertyItem("StyleManager", "StyleManager", "Metro", "Gets or sets the stylemanager for the control."),
                new DesignerActionPropertyItem("Style", "Style", "Metro", "Gets or sets the style."),

                new DesignerActionHeaderItem("Informations"),
                new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
                new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

                new DesignerActionHeaderItem("Misc"),
                new DesignerActionPropertyItem("Active", "Active", "Misc", "Gets or sets a value indicating whether the ToolTip is currently active."),
                new DesignerActionPropertyItem("AutomaticDelay", "AutomaticDelay", "Misc", "Gets or sets the automatic delay for the ToolTip."),
                new DesignerActionPropertyItem("AutoPopDelay", "AutoPopDelay", "Misc", "Gets or sets the period of time the ToolTip remains visible if the pointer is stationary on a control with specified ToolTip text."),
                new DesignerActionPropertyItem("InitialDelay", "InitialDelay", "Misc", "Gets or sets the time that passes before the ToolTip appears."),
                new DesignerActionPropertyItem("StripAmpersands", "StripAmpersands", "Misc", "Gets or sets a value that determines how ampersand (&) characters are treated."),
                new DesignerActionPropertyItem("UseAnimation", "UseAnimation", "Misc", "Gets or sets a value determining whether an animation effect should be used when displaying the ToolTip."),
                new DesignerActionPropertyItem("UseFading", "UseFading", "Appearance", "Gets or sets a value determining whether a fade effect should be used when displaying the ToolTip."),
            };
            return items;
        }
    }

    #endregion
}