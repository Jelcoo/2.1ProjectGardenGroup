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
            LoginScreen screen = new LoginScreen();
            svMainContent.Content = screen;
        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            Dashboard screen = new Dashboard();
            svMainContent.Content = screen;
        }

        private void ticketsButton_Click(object sender, RoutedEventArgs e)
        {
            TicketOverview screen = new TicketOverview(svMainContent);
            svMainContent.Content = screen;
        }

        private void employeesButton_Click(object sender, RoutedEventArgs e)
        {
            UserManagement screen = new UserManagement();
            svMainContent.Content = screen;
        }
    }
}
