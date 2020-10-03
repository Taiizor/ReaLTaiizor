#region Imports

using System.Drawing;
using ReaLTaiizor.Colors;
using System.Collections.Generic;

#endregion

namespace ReaLTaiizor.Extension.Poison
{
    #region PoisonPensExtension

    public sealed class PoisonPens
    {
        private static Dictionary<string, Pen> poisonPens = new Dictionary<string, Pen>();
        private static Pen GetSavePen(string key, Color color)
        {
            lock (poisonPens)
            {
                if (!poisonPens.ContainsKey(key))
                    poisonPens.Add(key, new Pen(color, 1f));

                return poisonPens[key].Clone() as Pen;
            }
        }

        public static Pen Black
        {
            get
            {
                return GetSavePen("Black", PoisonColors.Black);
            }
        }

        public static Pen White
        {
            get
            {
                return GetSavePen("White", PoisonColors.White);
            }
        }

        public static Pen Silver
        {
            get
            {
                return GetSavePen("Silver", PoisonColors.Silver);
            }
        }

        public static Pen Blue
        {
            get
            {
                return GetSavePen("Blue", PoisonColors.Blue);
            }
        }

        public static Pen Green
        {
            get
            {
                return GetSavePen("Green", PoisonColors.Green);
            }
        }

        public static Pen Lime
        {
            get
            {
                return GetSavePen("Lime", PoisonColors.Lime);
            }
        }

        public static Pen Teal
        {
            get
            {
                return GetSavePen("Teal", PoisonColors.Teal);
            }
        }

        public static Pen Orange
        {
            get
            {
                return GetSavePen("Orange", PoisonColors.Orange);
            }
        }

        public static Pen Brown
        {
            get
            {
                return GetSavePen("Brown", PoisonColors.Brown);
            }
        }

        public static Pen Pink
        {
            get
            {
                return GetSavePen("Pink", PoisonColors.Pink);
            }
        }

        public static Pen Magenta
        {
            get
            {
                return GetSavePen("Magenta", PoisonColors.Magenta);
            }
        }

        public static Pen Purple
        {
            get
            {
                return GetSavePen("Purple", PoisonColors.Purple);
            }
        }

        public static Pen Red
        {
            get
            {
                return GetSavePen("Red", PoisonColors.Red);
            }
        }

        public static Pen Yellow
        {
            get
            {
                return GetSavePen("Yellow", PoisonColors.Yellow);
            }
        }
    }

    #endregion
}