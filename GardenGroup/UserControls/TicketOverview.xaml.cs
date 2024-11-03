using Logic;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.UserControls
{
    /// <summary>
    /// Interaction logic for TicketOverview.xaml
    /// </summary>
    public partial class TicketOverview : UserControl
    {
        public ObservableCollection<Ticket> Tickets { get; set; }
        private TicketLogic ticketLogic;

        public TicketOverview()
        {
            InitializeComponent();
            Tickets = new ObservableCollection<Ticket>();
            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ticketLogic = new TicketLogic();
            List<Ticket> tickets = ticketLogic.GetTicketsEmployees();

            Tickets.Clear();
            foreach (var ticket in tickets)
            {
                Tickets.Add(ticket);
            }
            //ticketLogic.ChangeTicketStatus("b228c211-6b6e-4807-a41f-a515cc769be4", Model.Enums.Status_Enum.Closed);
        }

        private void tbFilterInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TicketList.ItemsSource);
            view.Filter = item => ((Ticket)item).title.ToLower().Contains(tbFilterInput.Text.ToLower());

            if (tbFilterInput.Text.Length >= 3)
            {
                if (filterType.Text == "Title")
                {
                    List<Ticket> tickets = ticketLogic.SearchTickets(tbFilterInput.Text);
                    Tickets.Clear();
                    foreach (var ticket in tickets)
                    {
                        Tickets.Add(ticket);
                    }
                }
            }
        }

        private void ViewTicketDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedTicket = (Ticket)((Button)sender).DataContext;

            if (selectedTicket != null)
            {
                // Retrieve the logged-in user from ApplicationStore
                Employee loggedInUser = ApplicationStore.GetInstance().getLoggedInUser();

                // Pass the ticket ID and logged-in user to TicketDetail
                var ticketDetail = new TicketDetail(selectedTicket._id.ToString(), loggedInUser);
                ticketDetail.ShowDialog();
            }
        }
    }
}
