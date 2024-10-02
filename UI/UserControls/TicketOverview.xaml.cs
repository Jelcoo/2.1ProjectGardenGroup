using DAL;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls
{
	/// <summary>
	/// Interaction logic for TicketOverview.xaml
	/// </summary>
	public partial class TicketOverview : UserControl
	{
		public TicketOverview()
		{
			InitializeComponent();
		}

		private void IncidentButton_Click(object sender, RoutedEventArgs e)
		{
			TicketBll ticketBll = new TicketBll();
			TicketDao ticketDao = new TicketDao();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}