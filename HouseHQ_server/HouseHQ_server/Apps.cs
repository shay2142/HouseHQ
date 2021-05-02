using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HouseHQ_server
{
    class Apps
    {
        public Dictionary<string, app> getAppsOnPC()
        {
            var apps = new Dictionary<string, app>();
            // search in: LocalMachine_32
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        string DisplayName = (string)subkey.GetValue("DisplayName");
                        string InstallLocation = (string)subkey.GetValue("InstallLocation");
                        string DisplayIcon = (string)subkey.GetValue("DisplayIcon");

                        if (DisplayName != null && InstallLocation != null && DisplayIcon != null && DisplayName != "" && InstallLocation != "" && DisplayIcon != "")
                        {
                            apps.Add(DisplayName,
                                new app
                                {
                                    appName = DisplayName,
                                    folder = InstallLocation,
                                    EXE_File = DisplayIcon
                                });
                        }
                    }
                }
            }
            // search in: LocalMachine_64
            registry_key = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        string DisplayName = (string)subkey.GetValue("DisplayName");
                        string InstallLocation = (string)subkey.GetValue("InstallLocation");
                        string DisplayIcon = (string)subkey.GetValue("DisplayIcon");

                        if (DisplayName != null && InstallLocation != null && DisplayIcon != null && DisplayName != "" && InstallLocation != "" && DisplayIcon != "")
                        {
                            apps.Add(DisplayName,
                                new app
                                {
                                    appName = DisplayName,
                                    folder = InstallLocation,
                                    EXE_File = DisplayIcon
                                });
                        }
                    }
                }
            }
            // search in: CurrentUser
            registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.CurrentUser.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        string DisplayName = (string)subkey.GetValue("DisplayName");
                        string InstallLocation = (string)subkey.GetValue("InstallLocation");
                        string DisplayIcon = (string)subkey.GetValue("DisplayIcon");

                        if (DisplayName != null && InstallLocation != null && DisplayIcon != null && DisplayName != "" && InstallLocation != "" && DisplayIcon != "")
                        {
                            apps.Add(DisplayName,
                                new app
                                {
                                    appName = DisplayName,
                                    folder = InstallLocation,
                                    EXE_File = DisplayIcon
                                });
                        }
                    }
                }
            }
            return apps;
        }
    }

    public class app
    {
        public string appName { get; set; }
        public string folder { get; set; }
        public string EXE_File { get; set; }
    }
}
