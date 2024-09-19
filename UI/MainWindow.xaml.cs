using System;
using System.Linq;
using System.Windows;
using MongoDB.Driver;
using MongoDB.Bson;
using Logic;
using Model;
using System.Windows.Controls;
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

			UserControl1 userControl1 = new UserControl1();
			//svMainWindow.Content = userControl1;
			gMainWindow.Children.Add(userControl1);
			Grid.SetColumn(userControl1, 0);
			Grid.SetRow(userControl1, 1);
		}
	}
}