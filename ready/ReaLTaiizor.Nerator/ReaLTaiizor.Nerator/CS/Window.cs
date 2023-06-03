using ReaLTaiizor.Nerator.UI;
using System.Windows.Forms;

namespace ReaLTaiizor.Nerator.CS
{
    public static class Window
    {
        public enum WindowType
        {
            EX,
            DARK,
            LIGHT
        }

        public static WindowType GetWindowMode(string Type)
        {
            return Type switch
            {
                "EX" => WindowType.EX,
                "DARK" => WindowType.DARK,
                _ => WindowType.LIGHT,
            };
        }

        public static string GetWindowMode(WindowType Type)
        {
            return Type switch
            {
                WindowType.EX => "EX",
                WindowType.DARK => "DARK",
                _ => "LIGHT",
            };
        }

        public static Form OpenWindowMode(WindowType Type)
        {
            return Type switch
            {
                WindowType.EX => new EX(),
                WindowType.DARK => new DARK(),
                _ => new LIGHT(),
            };
        }
    }
}