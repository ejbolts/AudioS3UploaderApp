using System.Configuration;
using System.Data;
using System.Windows;

namespace AudioRecordingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Detect if dark mode is enabled
            bool isDarkMode = ThemeHelper.IsDarkMode();

            // Load the correct resource dictionary based on the theme
            if (isDarkMode)
            {
                var darkTheme = new ResourceDictionary { Source = new Uri("DarkTheme.xaml", UriKind.Relative) };
                Resources.MergedDictionaries.Add(darkTheme);
            }
            else
            {
                var lightTheme = new ResourceDictionary { Source = new Uri("LightTheme.xaml", UriKind.Relative) };
                Resources.MergedDictionaries.Add(lightTheme);
            }
        }
    }


}
