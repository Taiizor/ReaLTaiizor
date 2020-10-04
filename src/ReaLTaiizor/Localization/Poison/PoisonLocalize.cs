#region Imports

using System.IO;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Localization.Poison
{
    #region PoisonLocalizeLocalization

    internal class PoisonLocalize
    {
        private DataSet languageDataset;

        public string DefaultLanguage()
        {
            return "en";
        }

        public string CurrentLanguage()
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
            importManifestResource(ctrlName);
        }

        public PoisonLocalize(Control ctrl)
        {
            importManifestResource(ctrl.Name);
        }

        private void importManifestResource(string ctrlName)
        {
            Assembly callingAssembly = Assembly.GetEntryAssembly();
            string localizationFilename = "";
            Stream xmlStream = null;
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
                languageDataset = new DataSet();
            }

            if (xmlStream != null)
            {
                DataSet importDataset = new DataSet();
                importDataset.ReadXml(xmlStream);

                languageDataset.Merge(importDataset);
                xmlStream.Close();
            }
        }

        private string convertVar(object var)
        {
            if (var == null)
            {
                return "";
            }

            return var.ToString();
        }

        public string translate(string key)
        {
            if ((string.IsNullOrEmpty(key)))
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

        public string translate(string key, object var1)
        {
            string str = translate(key);
            return str.Replace("#1", convertVar(var1));
        }

        public string translate(string key, object var1, object var2)
        {
            string str = translate(key);
            str = str.Replace("#1", convertVar(var1));
            return str.Replace("#2", convertVar(var2));
        }
        public string getValue(string key, object var1, object var2, object var3)
        {
            string str = translate(key);
            str = str.Replace("#1", convertVar(var1));
            str = str.Replace("#2", convertVar(var2));
            return str.Replace("#3", convertVar(var3));
        }
        public string getValue(string key, object var1, object var2, object var3, object var4)
        {
            string str = translate(key);
            str = str.Replace("#1", convertVar(var1));
            str = str.Replace("#2", convertVar(var2));
            str = str.Replace("#3", convertVar(var3));
            return str.Replace("#4", convertVar(var4));
        }
        public string getValue(string key, object var1, object var2, object var3, object var4, object var5)
        {
            string str = translate(key);
            str = str.Replace("#1", convertVar(var1));
            str = str.Replace("#2", convertVar(var2));
            str = str.Replace("#3", convertVar(var3));
            str = str.Replace("#4", convertVar(var4));
            return str.Replace("#5", convertVar(var5));
        }
    }

    #endregion
}