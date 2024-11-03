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
			// Verkrijg de huidige weergave van de DataGrid als CollectionView
			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TicketList.ItemsSource);
			string filterText = tbFilterInput.Text.ToLower();

			// Controleer of het filtertekstveld leeg is
			if (string.IsNullOrWhiteSpace(filterText))
			{
				// Reset het filter en verberg de melding
				view.Filter = null;
				NoResultsOverlay.Visibility = Visibility.Collapsed;
			}
			else
			{
				// Stel een filter in dat de tickets vergelijkt met de ingevoerde tekst
				view.Filter = item =>
				{
					// Controleer of het item een Ticket-object is
					if (item is Ticket ticket)
					{
						// Controleer of het ticket voldoet aan de filtercriteria
						return MatchesFilter(ticket, filterText);
					}

					// Als het item geen Ticket is, wordt het niet weergegeven in de DataGrid
					return false;
				};

				// Controleer of er geen resultaten zijn na het toepassen van het filter
				if (view.IsEmpty)
				{
					// Toon de overlay als er geen resultaten zijn
					NoResultsOverlay.Visibility = Visibility.Visible;
				}
				else
				{
					// Verberg de overlay als er resultaten zijn
					NoResultsOverlay.Visibility = Visibility.Collapsed;
				}
			}; view.Refresh();
		}

		private bool MatchesFilter(Ticket ticket, string filterText)
		{
			switch (filterType.Text)
			{
				case "Title":
					return ticket.title.ToLower().Contains(filterText);
				case "Status":
					return ticket.status.ToString().ToLower().Contains(filterText);
				case "Assigned to":
					return ticket.assigned_to.name.ToLower().Contains(filterText);
				case "Priority":
					return ticket.priority.ToString().ToLower().Contains(filterText);
				case "Created at":
					return ticket.created_at.ToString().ToLower().Contains(filterText);
				case "AND (&) OR (|)":
					return ApplyComplexFilter(ticket, filterText);

				default:
					return false;
			}
		}

		private bool ApplyComplexFilter(Ticket ticket, string filterText)
		{
			// Split de filtertekst in AND-segmenten
			string[] andSegments = filterText.Split(new string[] { " and ", "&" }, System.StringSplitOptions.None);
			bool andResult = andSegments.All(segment => SegmentMatches(ticket, segment.Trim()));

			// Controleer of er OR-operatoren in de filtertekst aanwezig zijn
			if (filterText.Contains(" or ") || filterText.Contains("|"))
			{
				string[] orSegments = filterText.Split(new string[] { " or ", "|" }, System.StringSplitOptions.None);
				return orSegments.Any(segment => SegmentMatches(ticket, segment.Trim()));
			}

			return andResult; // Retourneer het AND-resultaat als er geen OR-segmenten zijn
		}

		private bool SegmentMatches(Ticket ticket, string segment)
		{
			// Controleer of het segment voorkomt in een van de ticket eigenschappen
			bool titleMatch = ticket.title.ToLower().Contains(segment);
			bool statusMatch = ticket.status.ToString().ToLower().Contains(segment);
			bool assignedToMatch = ticket.assigned_to.name.ToLower().Contains(segment);
			bool priorityMatch = ticket.priority.ToString().ToLower().Contains(segment);
			bool createdAtMatch = ticket.created_at.ToString().ToLower().Contains(segment);

			// Retourneer true als het segment in een van de eigenschappen voorkomt
			return titleMatch || statusMatch || assignedToMatch || priorityMatch || createdAtMatch;
		}

		private void CloseNoResultsOverlay(object sender, RoutedEventArgs e)
		{
			// Verberg de overlay en reset het filter
			NoResultsOverlay.Visibility = Visibility.Collapsed;
			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TicketList.ItemsSource);
			view.Filter = null; // Verwijder het filter om de volledige lijst weer te geven
			tbFilterInput.Clear(); // Maak het invoerveld leeg
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
	}
}
