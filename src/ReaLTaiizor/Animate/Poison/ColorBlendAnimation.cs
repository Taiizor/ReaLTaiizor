#region Imports

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Animate.Poison
{
    #region ColorBlendAnimationAnimate

    public sealed class ColorBlendAnimation : Animate
    {
        private double percent = 1;

        public void Start(Control control, string property, Color targetColor, int duration)
        {
            if (duration == 0)
            {
                duration = 1;
            }

            Start(control, transitionType, 2 * duration,
                delegate
                {
                    Color controlColor = GetPropertyValue(property, control);
                    Color newColor = DoColorBlend(controlColor, targetColor, 0.1 * (percent / 2));

                    PropertyInfo prop = control.GetType().GetProperty(property);
                    MethodInfo method = prop.GetSetMethod(true);
                    method.Invoke(control, new object[] { newColor });
                },
                delegate
                {
                    Color controlColor = GetPropertyValue(property, control);

                    if (controlColor.A.Equals(targetColor.A) &&
                        controlColor.R.Equals(targetColor.R) &&
                        controlColor.G.Equals(targetColor.G) &&
                        controlColor.B.Equals(targetColor.B))
                    {
                        return true;
                    }

                    return false;
                });
        }

        private Color DoColorBlend(Color startColor, Color targetColor, double ratio)
        {
            percent += 0.2;

            int a = (int)Math.Round((startColor.A * (1 - ratio)) + (targetColor.A * ratio));
            int r = (int)Math.Round((startColor.R * (1 - ratio)) + (targetColor.R * ratio));
            int g = (int)Math.Round((startColor.G * (1 - ratio)) + (targetColor.G * ratio));
            int b = (int)Math.Round((startColor.B * (1 - ratio)) + (targetColor.B * ratio));
            return Color.FromArgb(a, r, g, b);
        }

        private static Color GetPropertyValue(string pName, Control control)
        {
            Type type = control.GetType();
            string propertyName = pName;

            BindingFlags flags = BindingFlags.GetProperty;
            Binder binder = null;
            object[] args = null;
            object value = type.InvokeMember(propertyName, flags, binder, control, args);

            return (Color)value;
        }
    }

    #endregion
}