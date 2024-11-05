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
		private ScrollViewer svMainContent;
		public ObservableCollection<Ticket> Tickets { get; set; }
		TicketLogic ticketLogic;

		public TicketOverview(ScrollViewer svMainContent)
		{
			InitializeComponent();
			this.svMainContent = svMainContent;
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
			// Haalt de huidige weergave van de DataGrid op als een CollectionView.
			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TicketList.ItemsSource);

			// Zet de invoer in kleine letters voor eenvoudiger vergelijken.
			string filterText = tbFilterInput.Text.ToLower();

			// Stel de filterfunctie voor de CollectionView in.
			view.Filter = item =>
			{
				if (item is Ticket ticket)
				{
					// Definieer bools voor match per eigenschap.
					bool titleMatch = ticket.title.ToLower().Contains(filterText);
					bool statusMatch = ticket.status.ToString().ToLower().Contains(filterText);
					bool assignedToMatch = ticket.assigned_to.name.ToLower().Contains(filterText);

					// Toepassen van de geselecteerde filterlogica.
					switch (filterType.Text)
					{
						case "Title":
							return titleMatch;
						case "Status":
							return statusMatch;
						case "Assigned to":
							return assignedToMatch;
						case "AND (&) OR (|)":
							return ApplyComplexFilter(ticket, filterText);
						default:
							return false;
					}
				}
				return false;
			};

			view.Refresh(); // Ververs de weergave om de filterresultaten toe te passen.
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

			// Retourneer true als het segment in een van de eigenschappen voorkomt.
			return titleMatch || statusMatch || assignedToMatch;
		}

		private void IncidentButton_Click(object sender, RoutedEventArgs e)
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
	}
}