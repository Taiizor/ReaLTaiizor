using System.Linq;
using System.Windows.Forms;

namespace ReaLTaiizor.UI.Helpers
{
    public static class FormHelper
    {
        public static Form Open(string[] Arg)
        {
            return $"{Arg.FirstOrDefault()}".ToLowerInvariant() switch
            {
                "1" or "form1" => new Form1(),
                "2" or "form2" => new Form2(),
                "3" or "form3" or "air" => new Form3(),
                "4" or "form4" or "dungeon" => new Form4(),
                "5" or "form5" or "dream" => new Form5(),
                "6" or "form6" or "ribbon" => new Form6(),
                "7" or "form7" or "space" => new Form7(),
                "8" or "form8" or "thunder" => new Form8(),
                "9" or "form9" or "sky" => new Form9(),
                "10" or "form10" or "moon" => new Form10(),
                "11" or "form11" or "alone" => new Form11(),
                "12" or "form12" or "fox" => new Form12(),
                "13" or "form13" or "forever" => new Form13(),
                "14" or "form14" or "hope" => new Form14(),
                "15" or "form15" or "lost" => new Form15(),
                "16" or "form16" or "royal" => new Form16(),
                "17" or "form17" or "material" => new Form17(),
                "18" or "form18" or "night" => new Form18(),
                "19" or "form19" or "metro" => new Form19(),
                "20" or "form20" or "poison" => new Form20(),
                "21" or "form21" or "crown" => new Form21(),
                "22" or "form22" or "parrot" => new Form22(),
                "23" or "form23" or "cyber" => new Form23(),
                _ => new Form17(),
            };
        }
    }
}