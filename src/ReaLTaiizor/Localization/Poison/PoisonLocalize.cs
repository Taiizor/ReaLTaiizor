#region Imports

using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Localization.Poison
{
    #region PoisonLocalizeLocalization

    internal class PoisonLocalize
    {
        private DataSet languageDataset;

        public static string DefaultLanguage()
        {
            return "en";
        }

        public static string CurrentLanguage()
        {
            string language = Application.CurrentCulture.TwoLetterISOLanguageName;
            //string language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
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
            Assembly callingAssembly = Assembly.GetEntryAssembly();
            Stream xmlStream = null;
            string localizationFilename;
            if (callingAssembly != null)
            {
                localizationFilename = callingAssembly.GetName().Name + ".Localization.Poison." + CurrentLanguage() + "." + ctrlName + ".xml";
                xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename);
            }

            if (xmlStream == null)
            {
                callingAssembly = Assembly.GetCallingAssembly();
                localizationFilename = callingAssembly.GetName().Name + ".Localization.Poison." + CurrentLanguage() + "." + ctrlName + ".xml";
                xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename);

                if (xmlStream == null)
                {
                    localizationFilename = callingAssembly.GetName().Name + ".Localization.Poison." + DefaultLanguage() + "." + ctrlName + ".xml";
                    xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename);
                }
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
                return "&" + key;
            }

            if (languageDataset.Tables["Localization"] == null)
            {
                return "&" + key;
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

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5, object var6)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            str = str.Replace("#5", ConvertVar(var5));
            return str.Replace("#6", ConvertVar(var6));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5, object var6, object var7)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            str = str.Replace("#5", ConvertVar(var5));
            str = str.Replace("#6", ConvertVar(var6));
            return str.Replace("#7", ConvertVar(var7));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5, object var6, object var7, object var8)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            str = str.Replace("#5", ConvertVar(var5));
            str = str.Replace("#6", ConvertVar(var6));
            str = str.Replace("#7", ConvertVar(var7));
            return str.Replace("#8", ConvertVar(var8));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5, object var6, object var7, object var8, object var9)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            str = str.Replace("#5", ConvertVar(var5));
            str = str.Replace("#6", ConvertVar(var6));
            str = str.Replace("#7", ConvertVar(var7));
            str = str.Replace("#8", ConvertVar(var8));
            return str.Replace("#9", ConvertVar(var9));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5, object var6, object var7, object var8, object var9, object var10)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            str = str.Replace("#5", ConvertVar(var5));
            str = str.Replace("#6", ConvertVar(var6));
            str = str.Replace("#7", ConvertVar(var7));
            str = str.Replace("#8", ConvertVar(var8));
            str = str.Replace("#9", ConvertVar(var9));
            return str.Replace("#10", ConvertVar(var10));
        }
    }

    #endregion
}