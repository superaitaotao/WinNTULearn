using System.Windows;

namespace WinNTULearn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();

            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();

            //SettingWindow settingWindow = new SettingWindow();
            //settingWindow.Show();
        }
    }
}
