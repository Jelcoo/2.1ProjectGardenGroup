using System.Windows;
using UI.UserControls;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            svMainContent.Content = loginScreen;
        }
    }
}
