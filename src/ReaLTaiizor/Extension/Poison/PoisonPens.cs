#region Imports

using ReaLTaiizor.Colors;
using System.Collections.Generic;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Extension.Poison
{
    #region PoisonPensExtension

    public sealed class PoisonPens
    {
        private static readonly Dictionary<string, Pen> poisonPens = new();
        private static Pen GetSavePen(string key, Color color)
        {
            lock (poisonPens)
            {
                if (!poisonPens.ContainsKey(key))
                {
                    poisonPens.Add(key, new(color, 1f));
                }

                return poisonPens[key].Clone() as Pen;
            }
        }

        public static Pen Black => GetSavePen("Black", PoisonColors.Black);

        public static Pen White => GetSavePen("White", PoisonColors.White);

        public static Pen Silver => GetSavePen("Silver", PoisonColors.Silver);

        public static Pen Blue => GetSavePen("Blue", PoisonColors.Blue);

        public static Pen Green => GetSavePen("Green", PoisonColors.Green);

        public static Pen Lime => GetSavePen("Lime", PoisonColors.Lime);

        public static Pen Teal => GetSavePen("Teal", PoisonColors.Teal);

        public static Pen Orange => GetSavePen("Orange", PoisonColors.Orange);

        public static Pen Brown => GetSavePen("Brown", PoisonColors.Brown);

        public static Pen Pink => GetSavePen("Pink", PoisonColors.Pink);

        public static Pen Magenta => GetSavePen("Magenta", PoisonColors.Magenta);

        public static Pen Purple => GetSavePen("Purple", PoisonColors.Purple);

        public static Pen Red => GetSavePen("Red", PoisonColors.Red);

        public static Pen Yellow => GetSavePen("Yellow", PoisonColors.Yellow);
    }

    #endregion
}