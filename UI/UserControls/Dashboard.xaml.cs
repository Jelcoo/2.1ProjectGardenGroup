using System.Windows;
using System.Windows.Controls;
using Logic;

namespace UI.UserControls
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

        }



        private void ShowListBtn(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            TicketOverview ticketOverview = new TicketOverview();

            mainWindow.svMainContent.Content = ticketOverview;
        }
    }
}
