using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinNTULearn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyNotifyIcon.LeftClickCommand = new ShowHideWindowCommand();
            MyNotifyIcon.LeftClickCommandParameter = this;
        }

        private class ShowHideWindowCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                MainWindow window = (MainWindow)parameter;
                if (window.WindowState == WindowState.Minimized)
                {
                    window.WindowState = WindowState.Normal;
                    Debug.Write("minimized");
                }
                else
                {
                    window.WindowState = WindowState.Minimized;
                    Debug.Write("normal");
                }
                Debug.Write("Notify Clicked");
            }
        }
    }
}
