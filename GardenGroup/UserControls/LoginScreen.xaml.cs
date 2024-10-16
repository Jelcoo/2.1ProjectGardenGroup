using Logic;
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
        public LoginScreen()
        {
            InitializeComponent();
        }
        private void onLoginClick(object sender, RoutedEventArgs e)
        {
            string email = emailInput.Text;
            string password = passwordInput.Password;

            LoginLogic loginLogic = new LoginLogic();
            Employee userToLogin = loginLogic.verifyLogin(email, password);
            if (userToLogin == null)
            {
                errorLabel.Content = "The username or password is not valid";
                return;
            }

            errorLabel.Content = "It works";
        }
    }
}
