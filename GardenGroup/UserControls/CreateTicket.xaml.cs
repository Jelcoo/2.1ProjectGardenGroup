using DAL;
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
			FillRoleDropDown();
			FillPriorityDropDown();
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
			if (!IsFormValid())
			{
				return;
			}

			Ticket newTicket = CreateTicketFromForm();

			// exception handling
			try
			{
				ticketLogic.SaveTicket(newTicket);
				MessageBox.Show("Ticket successfully created!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
				ClearForm(); // Optionally clear the form
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred while saving the ticket: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		// validate the fields in form
		private bool IsFormValid()
		{
			if (Datepicker.SelectedDate == null)
			{
				MessageBox.Show("Please select a date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}

			if (priorityDropDown.SelectedItem == null)
			{
				MessageBox.Show("Please select a priority level.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			
			if(roleDropDown.SelectedItem == null)
			{
				MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtBoxDescription.Text))
			{
				MessageBox.Show("Please enter a description.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}

			return true;
		}

		// create a Ticket object from form data
		private Ticket CreateTicketFromForm()
		{
			PartialUser loggedInUser = new UserLogic().GetLoggedInPartialUser();

			return new Ticket
			{
				occurred_at = (DateTime)Datepicker.SelectedDate,
				priority = priorityDropDown.SelectedItem.ToString(),
				reported_by = loggedInUser, // Gebruik de PartialUser direct
				description = txtBoxDescription.Text,
				status = Status_Enum.Open.ToString(),
				created_at = DateTime.Now
			};
		}

		// method to clear the form after successful submission
		private void ClearForm()
		{
			Datepicker.SelectedDate = null;
			priorityDropDown.SelectedIndex = -1;
			txtBoxDescription.Clear();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			TicketOverview ticketOverviewScreen = new TicketOverview(svMainContent);
			svMainContent.Content = ticketOverviewScreen;
		}

	}
}
