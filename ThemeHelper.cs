using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecordingApp
{
    public static class ThemeHelper
    {
        public static bool IsDarkMode()
        {
            // Check the registry for system theme setting
            var registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");


            if (registryKey != null)
            {
                var value = registryKey.GetValue("AppsUseLightTheme");
                Console.WriteLine("Registry Value for AppsUseLightTheme: " + value);

                if (value != null)
                {
                    return (int)value == 0; // 0 means dark mode is enabled, 1 means light mode
                }
            }
            return false; // Default to light mode if not found
        }
    }
}
