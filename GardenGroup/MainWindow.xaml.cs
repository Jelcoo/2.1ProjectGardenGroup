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
            //LoginScreen screen = new LoginScreen();
            //Dashboard screen = new Dashboard();
            //UserManagement screen = new UserManagement();
            //TicketOverview screen = new TicketOverview();
            CreateTicket screen = new CreateTicket();
            svMainContent.Content = screen;
        }
    }
}
