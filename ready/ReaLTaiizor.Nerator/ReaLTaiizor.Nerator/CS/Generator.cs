using System;
using System.Linq;
using static ReaLTaiizor.Nerator.CS.Character;

namespace ReaLTaiizor.Nerator.CS
{
    public static class Generator
    {
        private static readonly Random RM = new();

        private const string AC_JB = "ABCDEFGHIJKLMNOPRSTUVYZQWX";
        private const string AC_JS = "abcdefghijklmnoprstuvyzqwx";
        private const string AC_BS = AC_JB + AC_JS;

        private const string SL_JN = "1234567890"; // "111222333444555666777888999000"
        private const string SL_JS = "!`'\"^+%&/=?-_@€£ß*-+#\\<|>;:.,~½£$({[]})Æé~";
        private const string SL_NS = SL_JN + SL_JS;

        public static string Create(int Length, AlphabeticType AC, SpecialType SL)
        {
            string Dictionary = null;

            switch (AC)
            {
                case AlphabeticType.BS:
                    Dictionary = AC_BS;
                    break;
                case AlphabeticType.JB:
                    Dictionary = AC_JB;
                    break;
                case AlphabeticType.JS:
                    Dictionary = AC_JS;
                    break;
            }
            switch (SL)
            {
                case SpecialType.NS:
                    Dictionary += SL_NS;
                    break;
                case SpecialType.JN:
                    Dictionary += SL_JN;
                    break;
                case SpecialType.JS:
                    Dictionary += SL_JS;
                    break;
            }
            //return Shuffle_Mode_1(Dictionary).Substring(0, Length);
            return Shuffle_Mode_3(Dictionary, Length);
        }

        /*
            private static string Shuffle_Mode_1(string Text)
            {
                char[] Array = Text.ToCharArray();
                int Number = Array.Length;
                while (Number > 1)
                {
                    Number--;
                    int Result = RM.Next(Number + 1);
                    char Value = Array[Result];
                    Array[Result] = Array[Number];
                    Array[Number] = Value;
                }
                return new string(Array);
            }

            private static string Shuffle_Mode_2(string Text)
            {
                string Result = null;
                int Size = Text.Length;
                int[] RandomArray = new int[Size];

                for (int C = 0; C < Size; C++)
                {
                    RandomArray[C] = C;
                }

                Shuffle_Helper(RandomArray);

                for (int C = 0; C < Size; C++)
                {
                    Result += Text[RandomArray[C]];
                }

                return Result;
            }
        */

        private static string Shuffle_Mode_3(string Text, int Length)
        {
            return new string(Enumerable.Repeat(new string(Enumerable.Repeat(Text, Text.Length).Select(TXT => TXT[RM.Next(TXT.Length)]).ToArray()), Length).Select(PWD => PWD[RM.Next(PWD.Length)]).ToArray());
        }

        /*
            private static void Shuffle_Helper(int[] Array)
            {
                int Size = Array.Length;
                int Random;
                int Temp;

                for (int C = 0; C < Size; C++)
                {
                    Random = C + (int)(RM.NextDouble() * (Size - C));

                    Temp = Array[Random];
                    Array[Random] = Array[C];
                    Array[C] = Temp;
                }
            }
        */
    }
}