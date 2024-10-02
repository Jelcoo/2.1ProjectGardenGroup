using Logic;
using Model.models;
using System.Windows;
using System.Windows.Controls;

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
