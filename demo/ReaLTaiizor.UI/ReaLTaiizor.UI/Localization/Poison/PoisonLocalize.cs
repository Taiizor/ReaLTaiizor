using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ReaLTaiizor.UI.Localization.Poison
{
    internal class PoisonLocalize
    {
        private DataSet languageDataset;

        public static string DefaultLanguage()
        {
            return "en";
        }

        public static string CurrentLanguage()
        {
            string language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            if (language.Length == 0)
            {
                language = DefaultLanguage();
            }

            return language.ToLower();
        }

        public PoisonLocalize(string ctrlName)
        {
            ImportManifestResource(ctrlName);
        }

        public PoisonLocalize(Control ctrl)
        {
            ImportManifestResource(ctrl.Name);
        }

        private void ImportManifestResource(string ctrlName)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            string localizationFilename = callingAssembly.GetName().Name + ".Localization.Poison." + CurrentLanguage() + "." + ctrlName + ".xml";
            Stream xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename);

            if (xmlStream == null)
            {
                localizationFilename = callingAssembly.GetName().Name + ".Localization.Poison." + DefaultLanguage() + "." + ctrlName + ".xml";
                xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename);
            }


            if (languageDataset == null)
            {
                languageDataset = new();
            }

            if (xmlStream != null)
            {
                DataSet importDataset = new();
                importDataset.ReadXml(xmlStream);

                languageDataset.Merge(importDataset);
                xmlStream.Close();
            }
        }

        private static string ConvertVar(object var)
        {
            if (var == null)
            {
                return "";
            }

            return var.ToString();
        }

        public string Translate(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "";
            }

            if (languageDataset == null)
            {
                return "~" + key;
            }

            if (languageDataset.Tables["Localization"] == null)
            {
                return "~" + key;
            }

            DataRow[] languageRows = languageDataset.Tables["Localization"].Select("Key='" + key + "'");
            if (languageRows.Length <= 0)
            {
                return "~" + key;
            }

            return languageRows[0]["Value"].ToString();
        }

        public string Translate(string key, object var1)
        {
            string str = Translate(key);
            return str.Replace("#1", ConvertVar(var1));
        }

        public string Translate(string key, object var1, object var2)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            return str.Replace("#2", ConvertVar(var2));
        }

        public string GetValue(string key, object var1, object var2, object var3)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            return str.Replace("#3", ConvertVar(var3));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            return str.Replace("#4", ConvertVar(var4));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            return str.Replace("#5", ConvertVar(var5));
        }
    }
}