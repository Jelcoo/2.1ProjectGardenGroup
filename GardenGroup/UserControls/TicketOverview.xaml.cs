using Logic;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace UI.UserControls
{
	/// <summary>
	/// Interaction logic for TicketOverview.xaml
	/// </summary>
	public partial class TicketOverview : UserControl
	{
		private ScrollViewer svMainContent;
		public ObservableCollection<Ticket> Tickets { get; set; }
		private TicketLogic ticketLogic;

		PartialUser loggedInUser = new UserLogic().GetLoggedInPartialUser();

		private DispatcherTimer _timer;
		private const int _delay = 500;

		public TicketOverview(ScrollViewer svMainContent)
		{
			InitializeComponent();
			this.svMainContent = svMainContent;
			Tickets = new ObservableCollection<Ticket>();
			ticketLogic = new TicketLogic();
			this.DataContext = this;

			// Create a time to act as a delay for the filtering logic.
			// This is ment to prevent calling the database on eacht character change while the user is still typing.
			_timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(_delay)
			};
			_timer.Tick += Timer_Tick; // Subscribe to the timer's Tick event
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			LoadTickets();
			//ticketLogic.ChangeTicketStatus("b228c211-6b6e-4807-a41f-a515cc769be4", Model.Enums.Status_Enum.Closed);
		}

		private void LoadTickets()
		{
			List<Ticket> tickets = ticketLogic.GetTicketsEmployees(loggedInUser);
			Tickets.Clear();
			foreach (var ticket in tickets)
			{
				Tickets.Add(ticket);
			}
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			// Stop the timer
			_timer.Stop();

			// Execute your filtering logic here
			string searchQuery = tbFilterInput.Text;
			ApplyFilter(searchQuery); // Your method to filter the data
		}

		private void tbFilterInput_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Restart the timer on text change
			_timer.Stop();
			_timer.Start();
		}

		private void ApplyFilter(string searchQuery)
		{
			// Haalt de huidige weergave van de DataGrid op als een CollectionView.
			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TicketList.ItemsSource);

			// reset the filter before applying a new one
			view.Filter = null;

			//check if user selected the "Full search" filter
			if (filterType.Text == "Full search")
			{
				if (searchQuery.Length == 0)
					LoadTickets();
				else
					SearchInDatabase(searchQuery);
				return;
			}

			// Stel de filterfunctie voor de CollectionView in.
			view.Filter = item =>
			{
				if (item is Ticket ticket)
				{
					// Zet de invoer in kleine letters voor eenvoudiger vergelijken.
					string filterText = tbFilterInput.Text.ToLower();
					return SwitchFilter(filterText, ticket);
				}
				return false;
			};

			view.Refresh(); // Ververs de weergave om de filterresultaten toe te passen.
		}

		private bool SwitchFilter(string filterText, Ticket ticket)
		{

			// Definieer bools voor match per eigenschap.
			bool titleMatch = ticket.title.ToLower().Contains(filterText);
			bool statusMatch = ticket.status.ToString().ToLower().Contains(filterText);
			bool assignedToMatch = ticket.assigned_to.name.ToLower().Contains(filterText);
			bool priorityMatch = ticket.priority.ToString().ToLower().Contains(filterText);

			// Toepassen van de geselecteerde filterlogica.
			switch (filterType.Text)
			{
				case "Title":
					return titleMatch;
				case "Status":
					return statusMatch;
				case "Assigned to":
					return assignedToMatch;
				case "Priority":
					return priorityMatch;
				case "AND (&) OR (|)":
					return ApplyComplexFilter(ticket, filterText);
				default:
					return false;
			}
		}

		private void SearchInDatabase(string searchQuery)
		{
			// Do not search if the search query is less than 3 characters
			if (searchQuery.Length >= 3)
			{
				List<Ticket> tickets = ticketLogic.SearchTickets(searchQuery, loggedInUser);
				Tickets.Clear();
				foreach (Ticket tkt in tickets)
				{
					Tickets.Add(tkt);
				}
			}
		}

		private bool ApplyComplexFilter(Ticket ticket, string filterText)
		{
			// Splitst de filtertekst op "and" en "&" om AND-segmenten te krijgen.
			string[] andSegments = filterText.Split(new string[] { " and ", "&" }, System.StringSplitOptions.None);
			bool andResult = andSegments.All(segment => SegmentMatches(ticket, segment.Trim()));

			// Controleert of er "or" of "|" in de filtertekst aanwezig is voor OR-segmenten.
			if (filterText.Contains(" or ") || filterText.Contains("|"))
			{
				string[] orSegments = filterText.Split(new string[] { " or ", "|" }, System.StringSplitOptions.None);

				foreach (string segment in orSegments)
				{
					if (SegmentMatches(ticket, segment.Trim()))
					{
						return true; // Retourneer true zodra een OR-segment overeenkomt.
					}
				}
				return false; // Retourneer false als geen enkel OR-segment overeenkomt.
			}

			return andResult; // Retourneer het resultaat van de AND-logica als er geen OR-segmenten zijn.
		}

		private bool SegmentMatches(Ticket ticket, string segment)
		{
			bool titleMatch = ticket.title.ToLower().Contains(segment);
			bool statusMatch = ticket.status.ToString().ToLower().Contains(segment);
			bool assignedToMatch = ticket.assigned_to.name.ToLower().Contains(segment);
			bool priorityMatch = ticket.priority.ToString().ToLower().Contains(segment);
			// Retourneer true als het segment in een van de eigenschappen voorkomt.
			return titleMatch || statusMatch || assignedToMatch || priorityMatch;
		}

		private void CreateTicketbtn_Click(object sender, RoutedEventArgs e)
		{
			if (svMainContent == null)
			{
				MessageBox.Show("ScrollViewer is niet ingesteld", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Maak een nieuwe instantie van de CreateTicket UserControl
			CreateTicket createTicketScreen = new CreateTicket(svMainContent);

			// Stel de Content van svMainContent in om te navigeren naar de CreateTicket pagina
			svMainContent.Content = createTicketScreen;
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
		private void CloseTicket_Click(object sender, RoutedEventArgs e)
		{
			var selectedTicket = (Ticket)((Button)sender).DataContext;
			if (selectedTicket != null)
			{
				CloseTicket(selectedTicket);
				LoadTickets();
			}
		}

		private void CloseTicket(Ticket ticket)
		{
			ticketLogic.ChangeTicketStatus(ticket._id, Status_Enum.Closed);
		}
	}
}