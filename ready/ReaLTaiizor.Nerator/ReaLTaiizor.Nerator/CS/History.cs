using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static ReaLTaiizor.Nerator.CS.Setting;
using static ReaLTaiizor.Nerator.CS.Variable;

namespace ReaLTaiizor.Nerator.CS
{
    public class History
    {
        public static string HistoryFileName => "History.json";
        public static long DefaultDateTime => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public static int DefaultDateTimeLength => DefaultDateTime.ToString().Length;
        public static string DefaultTime => "HH:mm:ss";
        public static string DefaultDate => "dd.MM.yyyy";
        public static int MaximumHistoryList => 100;

        public History(string HistoryFileName)
        {
            if (File.Exists(HistoryFileName))
            {
                Load(HistoryFileName);
            }
            else
            {
                Save(HistoryFileName);
            }
        }

        private void Load(string HistoryFileName)
        {
            string HS = File.ReadAllText(HistoryFileName);
            if (!string.IsNullOrEmpty(HS) && !string.IsNullOrWhiteSpace(HS))
            {
                Dictionary<string, string> History = JsonConvert.DeserializeObject<Dictionary<string, string>>(HS);
                foreach (string PKey in History.Keys)
                {
                    if (PKey.Length < MinimumPasswordLength || PKey.Length > MaximumPasswordLength)
                    {
                        Remove(HistoryFileName, PKey);
                        Load(HistoryFileName);
                        break;
                    }
                }
            }
        }

        public static Dictionary<string, string> Loader(string HistoryFileName)
        {
            string HS = File.ReadAllText(HistoryFileName);
            if (!string.IsNullOrEmpty(HS) && !string.IsNullOrWhiteSpace(HS))
            {
                Dictionary<string, string> History = JsonConvert.DeserializeObject<Dictionary<string, string>>(HS);
                foreach (string PKey in History.Keys)
                {
                    if (PKey.Length < MinimumPasswordLength || PKey.Length > MaximumPasswordLength || History[PKey].Length != DefaultDateTimeLength)
                    {
                        Remove(HistoryFileName, PKey);
                        return Loader(HistoryFileName);
                    }
                }
                return History;
            }
            return new Dictionary<string, string> { { "Error", GetString(DefaultDateTime, DefaultDateTime) } };
        }

        private static void Save(string HistoryFileName)
        {
            File.WriteAllText(HistoryFileName, "{}");
        }

        public static void Add(string HistoryFileName, string Password, long Time)
        {
            if (!File.Exists(HistoryFileName))
            {
                Save(HistoryFileName);
            }

            string HS = File.ReadAllText(HistoryFileName);
            Dictionary<string, string> History = JsonConvert.DeserializeObject<Dictionary<string, string>>(HS);

            if (!History.ContainsKey(Password) || (History.ContainsKey(Password) && GetLong(History[Password], DefaultDateTime) != Time))
            {
                History[Password] = GetString(DefaultDateTime, DefaultDateTime);

                File.WriteAllText(HistoryFileName, JsonConvert.SerializeObject(History, Formatting.Indented));
            }
        }

        public static bool Remove(string HistoryFileName, string Password)
        {
            try
            {
                if (File.Exists(HistoryFileName))
                {
                    string HS = File.ReadAllText(HistoryFileName);
                    if (!string.IsNullOrEmpty(HS) && !string.IsNullOrWhiteSpace(HS) && HS != "{}")
                    {
                        Dictionary<string, string> History = JsonConvert.DeserializeObject<Dictionary<string, string>>(HS);

                        if (History.ContainsKey(Password))
                        {
                            History.Remove(Password);
                            File.WriteAllText(HistoryFileName, JsonConvert.SerializeObject(History, Formatting.Indented));
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int ListPasswordCount { get; set; }
    }
}