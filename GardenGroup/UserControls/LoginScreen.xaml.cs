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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        UserLogic userLogic = new UserLogic();

        public LoginScreen()
        {
            InitializeComponent();
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailInput.Text;
            string password = passwordInput.Password;
            Employee userToLogin = userLogic.VerifyLogin(email, password);
            if (userToLogin == null)
            {
                errorLabel.Content = "The username or password is not valid";
                return;
            }

            ApplicationStore.GetInstance().setLoggedInUser(userToLogin);

            MainWindow window = Window.GetWindow(this) as MainWindow;
            Label? loggedInAsLabel = window.FindName("loggedInAsLabel") as Label;
            loggedInAsLabel.Content = "Welcome " + userToLogin.name + "! You are a " + userToLogin.role.ToString();
            Button? dashButton = window.FindName("dashboardButton") as Button;
            Button? ticketsButton = window.FindName("ticketsButton") as Button;
            Button? employeesButton = window.FindName("employeesButton") as Button;
            dashButton.Visibility = Visibility.Visible;
            ticketsButton.Visibility = Visibility.Visible;
            if (userToLogin.role == Role.ServiceDesk)
            {
                employeesButton.Visibility = Visibility.Visible;
            }

            window.svMainContent.Content = new Dashboard();
        }

        private void pwResetButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.svMainContent.Content = new PwResetLanding();
        }
    }
}
