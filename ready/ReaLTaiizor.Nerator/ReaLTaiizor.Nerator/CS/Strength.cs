using ReaLTaiizor.Enum.Poison;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ReaLTaiizor.Nerator.CS.Setting;

namespace ReaLTaiizor.Nerator.CS
{
    public class Strength
    {
        public enum ScoreType
        {
            Blank = 0,
            TooShort = 1,
            RequirementsNotMet = 2,
            VeryWeak = 3,
            Weak = 4,
            Fair = 5,
            Medium = 6,
            Strong = 7,
            VeryStrong = 8
        }

        public static ScoreType CheckScore1(string Password, int Score = 0, bool blnRequirement1Met = false, bool blnRequirement2Met = false)
        {
            bool blnMinLengthRequirementMet;

            if (Password.Length <= 0)
            {
                return ScoreType.Blank;
            }

            if (Password.Length < MinimumPasswordLength)
            {
                return ScoreType.TooShort;
            }
            else
            {
                Score++;
                blnMinLengthRequirementMet = true;
            }

            if (Password.Length >= 8)
            {
                Score++;
            }

            if (Password.Length >= 10)
            {
                Score++;
            }

            if (Regex.IsMatch(Password, @"[\d]", RegexOptions.ECMAScript))
            {
                Score++;
                blnRequirement1Met = true;
            }

            if (Regex.IsMatch(Password, @"[a-z]", RegexOptions.ECMAScript))
            {
                Score++;
                blnRequirement2Met = true;
            }

            if (Regex.IsMatch(Password, @"[A-Z]", RegexOptions.ECMAScript))
            {
                Score++;
                blnRequirement2Met = true;
            }

            if (Regex.IsMatch(Password, @"[~`!@#$%\^\&\*\(\)\-_\+=\[\{\]\}\|\\;:'\""<\,>\.\?\/£]", RegexOptions.ECMAScript))
            {
                Score++;
            }

            List<char> lstPass = Password.ToList();
            if (lstPass.Count >= 3)
            {
                for (int i = 2; i < lstPass.Count; i++)
                {
                    char charCurrent = lstPass[i];
                    if (charCurrent == lstPass[i - 1] && charCurrent == lstPass[i - 2] && Score >= 4)
                    {
                        Score++;
                    }
                }
            }

            if (!blnMinLengthRequirementMet || !blnRequirement1Met || !blnRequirement2Met)
            {
                return ScoreType.RequirementsNotMet;
            }

            return (ScoreType)Score;
        }

        public enum StrengthType
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public static class PasswordOptions
        {
            public static int RequiredLength => MinimumPasswordLength;
            public static int RequiredUniqueChars => 3;
            public static bool RequireNonAlphanumeric => true;
            public static bool RequireLowercase => true;
            public static bool RequireUppercase => true;
            public static bool RequireDigit => true;
        }

        public static int StrengthMode(StrengthType Type)
        {
            return Type switch
            {
                StrengthType.VeryWeak => 20,
                StrengthType.Weak => 40,
                StrengthType.Medium => 60,
                StrengthType.Strong => 80,
                StrengthType.VeryStrong => 100,
                _ => 0,
            };
        }

        public static ColorStyle StyleMode(int Type)
        {
            return Type switch
            {
                20 => ColorStyle.Red,
                40 => ColorStyle.Orange,
                60 => ColorStyle.Yellow,
                80 => ColorStyle.Blue,
                100 => ColorStyle.Green,
                _ => ColorStyle.White,
            };
        }

        public static StrengthType CheckScore2(string Password, int Score = 0)
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Password.Trim()))
            {
                return StrengthType.Blank;
            }

            if ((!IsValidPassword(Password) || !IsStrongPassword(Password)) && Password.Length <= 8)
            {
                return StrengthType.Weak;
            }

            if (HasMinimumLength(Password, MinimumPasswordLength))
            {
                Score++;
            }

            if (HasMinimumLength(Password, 8))
            {
                Score++;
            }

            if (HasUpperCaseLetter(Password) && HasLowerCaseLetter(Password))
            {
                Score++;
            }

            if (HasDigit(Password))
            {
                Score++;
            }

            if (HasSpecialChar(Password))
            {
                Score++;
            }

            return (StrengthType)Score;
        }

        private static bool IsStrongPassword(string Password)
        {
            return HasMinimumLength(Password, MinimumPasswordLength) && HasUpperCaseLetter(Password) && HasLowerCaseLetter(Password) && (HasDigit(Password) || HasSpecialChar(Password));
        }

        private static bool IsValidPassword(string Password)
        {
            return IsValidPassword(Password, PasswordOptions.RequiredLength, PasswordOptions.RequiredUniqueChars, PasswordOptions.RequireNonAlphanumeric, PasswordOptions.RequireLowercase, PasswordOptions.RequireUppercase, PasswordOptions.RequireDigit);
        }

        private static bool IsValidPassword(string Password, int requiredLength, int requiredUniqueChars, bool requireNonAlphanumeric, bool requireLowercase, bool requireUppercase, bool requireDigit)
        {
            if (!HasMinimumLength(Password, requiredLength))
            {
                return false;
            }

            if (!HasMinimumUniqueChars(Password, requiredUniqueChars))
            {
                return false;
            }

            if (requireNonAlphanumeric && !HasSpecialChar(Password))
            {
                return false;
            }

            if (requireLowercase && !HasLowerCaseLetter(Password))
            {
                return false;
            }

            if (requireUppercase && !HasUpperCaseLetter(Password))
            {
                return false;
            }

            if (requireDigit && !HasDigit(Password))
            {
                return false;
            }

            return true;
        }

        private static bool HasMinimumLength(string Password, int minLength)
        {
            return Password.Length >= minLength;
        }

        private static bool HasMinimumUniqueChars(string Password, int minUniqueChars)
        {
            return Password.Distinct().Count() >= minUniqueChars;
        }

        private static bool HasDigit(string Password)
        {
            return Password.Any(char.IsDigit);
        }

        private static bool HasSpecialChar(string Password)
        {
            // return Password.Any(C => char.IsPunctuation(C)) || Password.Any(C => char.IsSeparator(C)) || Password.Any(C => char.IsSymbol(C));
            return Password.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1;
        }

        private static bool HasUpperCaseLetter(string Password)
        {
            return Password.Any(char.IsUpper);
        }

        private static bool HasLowerCaseLetter(string Password)
        {
            return Password.Any(char.IsLower);
        }
    }
}