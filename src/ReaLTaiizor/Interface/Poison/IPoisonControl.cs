#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Manager;
using System;

#endregion

namespace ReaLTaiizor.Interface.Poison
{
    #region IPoisonControlInterface

    public interface IPoisonControl
    {
        event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        event EventHandler<PoisonPaintEventArgs> CustomPaint;
        event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;

        ColorStyle Style { get; set; }
        ThemeStyle Theme { get; set; }

        PoisonStyleManager StyleManager { get; set; }

        bool UseCustomBackColor { get; set; }
        bool UseCustomForeColor { get; set; }
        bool UseStyleColors { get; set; }
        bool UseSelectable { get; set; }
    }

    #endregion
}