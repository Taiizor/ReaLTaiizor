namespace ReaLTaiizor.Nerator.CS
{
    public class Character
    {
        public enum AlphabeticType
        {
            JB,
            JS,
            BS
        }

        public static AlphabeticType GetAlphabeticMode(string Type)
        {
            return Type switch
            {
                "JB" => AlphabeticType.JB,
                "JS" => AlphabeticType.JS,
                _ => AlphabeticType.BS,
            };
        }

        public static string GetAlphabeticMode(AlphabeticType Type)
        {
            return Type switch
            {
                AlphabeticType.JB => "JB",
                AlphabeticType.JS => "JS",
                _ => "BS",
            };
        }

        public enum SpecialType
        {
            JN,
            JS,
            NS
        }

        public static SpecialType GetSpecialMode(string Type)
        {
            return Type switch
            {
                "JN" => SpecialType.JN,
                "JS" => SpecialType.JS,
                _ => SpecialType.NS,
            };
        }

        public static string GetSpecialMode(SpecialType Type)
        {
            return Type switch
            {
                SpecialType.JN => "JN",
                SpecialType.JS => "JS",
                _ => "NS",
            };
        }
    }
}