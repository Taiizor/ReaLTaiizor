#region Imports

using ReaLTaiizor.Colors;
using System.Collections.Generic;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Extension.Poison
{
    #region PoisonBrushesExtension

    public sealed class PoisonBrushes
    {
        private static readonly Dictionary<string, SolidBrush> poisonBrushes = new();
        private static SolidBrush GetSaveBrush(string key, Color color)
        {
            lock (poisonBrushes)
            {
                if (!poisonBrushes.ContainsKey(key))
                {
                    poisonBrushes.Add(key, new SolidBrush(color));
                }

                return poisonBrushes[key].Clone() as SolidBrush;
            }
        }

        public static SolidBrush Black => GetSaveBrush("Black", PoisonColors.Black);

        public static SolidBrush White => GetSaveBrush("White", PoisonColors.White);

        public static SolidBrush Silver => GetSaveBrush("Silver", PoisonColors.Silver);

        public static SolidBrush Blue => GetSaveBrush("Blue", PoisonColors.Blue);

        public static SolidBrush Green => GetSaveBrush("Green", PoisonColors.Green);

        public static SolidBrush Lime => GetSaveBrush("Lime", PoisonColors.Lime);

        public static SolidBrush Teal => GetSaveBrush("Teal", PoisonColors.Teal);

        public static SolidBrush Orange => GetSaveBrush("Orange", PoisonColors.Orange);

        public static SolidBrush Brown => GetSaveBrush("Brown", PoisonColors.Brown);

        public static SolidBrush Pink => GetSaveBrush("Pink", PoisonColors.Pink);

        public static SolidBrush Magenta => GetSaveBrush("Magenta", PoisonColors.Magenta);

        public static SolidBrush Purple => GetSaveBrush("Purple", PoisonColors.Purple);

        public static SolidBrush Red => GetSaveBrush("Red", PoisonColors.Red);

        public static SolidBrush Yellow => GetSaveBrush("Yellow", PoisonColors.Yellow);

        public static SolidBrush Custom => GetSaveBrush("Custom", PoisonColors.Custom);
    }

    #endregion
}