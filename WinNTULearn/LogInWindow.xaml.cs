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
            LogInLabel.Content = "Logging in, please wait";
            NTUFectherResult logInResult = await new NTULearnFetcher().GetCourseListAsync();
            if ( logInResult.Type == NTUFetcherResultType.Success )
                LogInLabel.Content = "Logged in successfully!";
            else
                LogInLabel.Content = "Logged in failed...";
        }
    }
}
