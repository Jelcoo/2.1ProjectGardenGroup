using System.Windows;
using Logic;
using UI.UserControls;

namespace UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Databases databases;
		public MainWindow()
		{
			InitializeComponent();
			//databases = new Databases();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//var dbList = databases.Get_All_Databases();

			//foreach (var db in dbList)
			//{
			//	//listBox1.Items.Add(db.name);
			//}

			TicketOverview ticketOverview = new TicketOverview();
			svMainContent.Content = ticketOverview;
		}
	}
}