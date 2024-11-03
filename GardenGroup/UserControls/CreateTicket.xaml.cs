using Logic;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CreateTicket.xaml
    /// </summary>
    public partial class CreateTicket : UserControl
    {
        private TicketLogic ticketLogic;
		private ScrollViewer svMainContent;
		public CreateTicket(ScrollViewer svMainContent)
        {
            this.svMainContent = svMainContent;
			ticketLogic = new TicketLogic();
            InitializeComponent();
            FillEmployeeDropDown();
            FillRoleDropDown();
            FillPriorityDropDown();
        }
        public void FillEmployeeDropDown()
        {
            List<PartialUser> employees = ticketLogic.GetEmployees();
            foreach (PartialUser employee in employees)
            {
                reportedByDropdown.Items.Add(employee);
            }
        }
        public void FillRoleDropDown()
        {
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                roleDropDown.Items.Add(role);
            }
        }

        public void FillPriorityDropDown()
        {
            foreach (Priority priority in Enum.GetValues(typeof(Priority)))
            {
                priorityDropDown.Items.Add(priority);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Ticket newTicket = new Ticket();

            // Haal de waarden op van de verschillende velden in het formulier
            newTicket._id = Guid.NewGuid().ToString();
            newTicket.occurred_at = (DateTime)Datepicker.SelectedDate; // Datum uit DatePicker
            newTicket.reported_by = (PartialUser)reportedByDropdown.SelectedItem; // Gebruiker uit ComboBox (
            newTicket.priority = priorityDropDown.SelectedItem.ToString(); // Prioriteit uit ComboBox
            newTicket.description = txtBoxDescription.Text; // Beschrijving uit TextBox
            newTicket.status = Status_Enum.Open.ToString(); // 
            newTicket.created_at = DateTime.Now;

            // Verwerk het nieuwe ticket
            ticketLogic.SaveTicket(newTicket);

            MessageBox.Show("Ticket succesvol aangemaakt!", "Bevestiging", MessageBoxButton.OK, MessageBoxImage.Information);
        }

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			TicketOverview ticketOverviewScreen = new TicketOverview(svMainContent);
			svMainContent.Content = ticketOverviewScreen;
		}
	}
}
