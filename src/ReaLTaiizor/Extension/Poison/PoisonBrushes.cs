#region Imports

using System.Drawing;
using ReaLTaiizor.Colors;
using System.Collections.Generic;

#endregion

namespace ReaLTaiizor.Extension.Poison
{
    #region PoisonBrushesExtension

    public sealed class PoisonBrushes
    {
        private static Dictionary<string, SolidBrush> poisonBrushes = new Dictionary<string, SolidBrush>();
        private static SolidBrush GetSaveBrush(string key, Color color)
        {
            lock (poisonBrushes)
            {
                if (!poisonBrushes.ContainsKey(key))
                    poisonBrushes.Add(key, new SolidBrush(color));

                return poisonBrushes[key].Clone() as SolidBrush;
            }
        }

        public static SolidBrush Black
        {
            get
            {
                return GetSaveBrush("Black", PoisonColors.Black);
            }
        }

        public static SolidBrush White
        {
            get
            {
                return GetSaveBrush("White", PoisonColors.White);
            }
        }

        public static SolidBrush Silver
        {
            get
            {
                return GetSaveBrush("Silver", PoisonColors.Silver);
            }
        }

        public static SolidBrush Blue
        {
            get
            {
                return GetSaveBrush("Blue", PoisonColors.Blue);
            }
        }

        public static SolidBrush Green
        {
            get
            {
                return GetSaveBrush("Green", PoisonColors.Green);
            }
        }

        public static SolidBrush Lime
        {
            get
            {
                return GetSaveBrush("Lime", PoisonColors.Lime);
            }
        }

        public static SolidBrush Teal
        {
            get
            {
                return GetSaveBrush("Teal", PoisonColors.Teal);
            }
        }

        public static SolidBrush Orange
        {
            get
            {
                return GetSaveBrush("Orange", PoisonColors.Orange);
            }
        }

        public static SolidBrush Brown
        {
            get
            {
                return GetSaveBrush("Brown", PoisonColors.Brown);
            }
        }

        public static SolidBrush Pink
        {
            get
            {
                return GetSaveBrush("Pink", PoisonColors.Pink);
            }
        }

        public static SolidBrush Magenta
        {
            get
            {
                return GetSaveBrush("Magenta", PoisonColors.Magenta);
            }
        }

        public static SolidBrush Purple
        {
            get
            {
                return GetSaveBrush("Purple", PoisonColors.Purple);
            }
        }

        public static SolidBrush Red
        {
            get
            {
                return GetSaveBrush("Red", PoisonColors.Red);
            }
        }

        public static SolidBrush Yellow
        {
            get
            {
                return GetSaveBrush("Yellow", PoisonColors.Yellow);
            }
        }

        public static SolidBrush Custom
        {
            get
            {
                return GetSaveBrush("Custom", PoisonColors.Custom);
            }
        }
    }

    #endregion
}