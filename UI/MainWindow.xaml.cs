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
			LoginScreen loginScreen = new LoginScreen();
			svMainContent.Content = loginScreen;
		}
	}
}