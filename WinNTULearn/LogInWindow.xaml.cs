using System.Windows;

namespace WinNTULearn
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        async private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("button clicked");
            await new NTULearnFetcher().LogInAsync();
        }
    }
}
