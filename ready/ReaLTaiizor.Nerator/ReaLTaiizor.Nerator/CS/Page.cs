namespace ReaLTaiizor.Nerator.CS
{
    public static class Page
    {
        public enum PageType
        {
            Generate,
            History,
            Setting
        }

        public static PageType GetPageMode(string Type)
        {
            return Type switch
            {
                "History" => PageType.History,
                "Setting" => PageType.Setting,
                _ => PageType.Generate,
            };
        }

        public static string GetPageMode(PageType Type)
        {
            return Type switch
            {
                PageType.History => "History",
                PageType.Setting => "Setting",
                _ => "Generate",
            };
        }
    }
}