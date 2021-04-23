using System;
using System.Management;

namespace GameLauncher.App.Classes.SystemPlatform.Windows
{
    class ManagementSearcher
    {
        /* Searches for Installed Windows Updates */
        public static bool GetInstalledHotFix(string identification)
        {
            try
            {
                var search = new ManagementObjectSearcher("SELECT HotFixID FROM Win32_QuickFixEngineering");
                var collection = search.Get();

                foreach (ManagementObject quickFix in collection)
                {
                    Console.WriteLine("Updates installed: " + quickFix["HotFixID"].ToString());
                    if (quickFix["HotFixID"].ToString() == identification)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
