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
    /// Interaction logic for PwResetScreen.xaml
    /// </summary>
    public partial class PwResetScreen : UserControl
    {
        UserLogic userLogic = new UserLogic();

        public PwResetScreen()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailInput.Text;
            if (email == "")
            {
                errorLabel.Content = "Please enter an email address!";
                return;
            }
            Employee employee = userLogic.getEmployeeByEmail(email);
            if (employee != null)
            {
                userLogic.SendResetEmail(employee);
            }
        }
    }
}
