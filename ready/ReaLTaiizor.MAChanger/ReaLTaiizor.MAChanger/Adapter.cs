using Microsoft.Win32;
using System;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ReaLTaiizor.MAChanger
{
    internal class Adapter
    {
        public ManagementObject VAdapter;
        public string AdapterName;
        public string CustomName;
        public int DevNum;

        private Adapter(ManagementObject A, string AName, string CName, int DN)
        {
            VAdapter = A;
            AdapterName = AName;
            CustomName = CName;
            DevNum = DN;
        }

        public Adapter(NetworkInterface i) : this(i.Description) { }

        private Adapter(string AName)
        {
            AdapterName = AName;

            ManagementObjectSearcher Searcher = new("SELECT * FROM Win32_NetworkAdapter WHERE Name='" + AdapterName + "'");
            ManagementObjectCollection Found = Searcher.Get();
            VAdapter = Found.Cast<ManagementObject>().FirstOrDefault();

            try
            {
                Match Match = Regex.Match(VAdapter.Path.RelativePath, "\\\"(\\d+)\\\"$");
                DevNum = int.Parse(Match.Groups[1].Value);
            }
            catch
            {
                return;
            }

            CustomName = NetworkInterface.GetAllNetworkInterfaces().Where(o => o.Description == AdapterName).Select(o => " (" + o.Name + ")").FirstOrDefault();
        }

        private NetworkInterface ManagedAdapter => NetworkInterface.GetAllNetworkInterfaces().Where(ANI => ANI.Description == AdapterName).FirstOrDefault();

        public string GMAC
        {
            get
            {
                try
                {
                    return BitConverter.ToString(ManagedAdapter.GetPhysicalAddress().GetAddressBytes()).Replace("-", "").ToUpper();
                }
                catch
                {
                    return null;
                }
            }
        }

        private string RegistryKey => string.Format(@"SYSTEM\ControlSet001\Control\Class\{{4D36E972-E325-11CE-BFC1-08002BE10318}}\{0:D4}", DevNum);

        public string RegistryMAC
        {
            get
            {
                try
                {
                    using RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(RegistryKey, false);
                    return RegKey.GetValue("NetworkAddress").ToString();
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool SetRegistryMAC(string Value, string Title)
        {
            bool ShouldReenable = false;
            try
            {
                if (Value.Length > 0 && !ControlMAC(Value, false))
                {
                    throw new Exception(Value + " Is Not a Valid MAC Address!");
                }
                else
                {
                    using RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(RegistryKey, true);
                    if (RegKey == null)
                    {
                        throw new Exception("Registry Key Could Not Be Opened!");
                    }
                    else if ((RegKey.GetValue("AdapterModel") as string) != AdapterName && (RegKey.GetValue("DriverDesc") as string) != AdapterName)
                    {
                        throw new Exception("Adapter Not Found in Registry!");
                    }
                    else
                    {
                        string Question = Value.Length > 0 ? "MAC Address of Adapter {0}: {1}\nChange New MAC Address To {2}?" : "Retract MAC Address Settings of Adapter {0}?";
                        DialogResult Proceed = MessageBox.Show(string.Format(Question, ToString(), GMAC, Value), Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Proceed != DialogResult.Yes)
                        {
                            return false;
                        }
                        else
                        {
                            uint Result = (uint)VAdapter.InvokeMethod("Disable", null);
                            if (Result != 0)
                            {
                                throw new Exception("Adapter Could Not Be Disabled!");
                            }
                            else
                            {
                                ShouldReenable = true;

                                if (Value.Length > 0)
                                {
                                    RegKey.SetValue("NetworkAddress", Value, RegistryValueKind.String);
                                }
                                else if (!string.IsNullOrEmpty(RegKey.GetValue("NetworkAddress") as string))
                                {
                                    RegKey.DeleteValue("NetworkAddress");
                                }

                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                return false;
            }
            finally
            {
                if (ShouldReenable)
                {
                    uint Result = (uint)VAdapter.InvokeMethod("Enable", null);
                    if (Result != 0)
                    {
                        MessageBox.Show("Adapter Could Not Be Reactivated!");
                    }
                }
            }
        }

        public override string ToString()
        {
            return AdapterName + CustomName;
        }

        public static string CreateMAC()
        {
            Random RM = new();

            byte[] Bytes = new byte[6];
            RM.NextBytes(Bytes);

            Bytes[0] = (byte)(Bytes[0] | 0x02);
            Bytes[0] = (byte)(Bytes[0] & 0xfe);

            return MACToString(Bytes);
        }

        public static bool ControlMAC(string MAC, bool Actual)
        {
            if (MAC.Length != 12)
            {
                return false;
            }
            else if (MAC != MAC.ToUpper())
            {
                return false;
            }
            else if (!Regex.IsMatch(MAC, "^[0-9A-F]*$"))
            {
                return false;
            }
            else if (Actual)
            {
                return true;
            }
            else
            {
                char C = MAC[1];
                return C is 'A' or 'E' or '2' or '6';
            }
        }

        public static bool ControlMAC(byte[] Bytes, bool Actual)
        {
            return ControlMAC(Adapter.MACToString(Bytes), Actual);
        }

        private static string MACToString(byte[] Bytes)
        {
            return BitConverter.ToString(Bytes).Replace("-", "").ToUpper();
        }
    }
}
