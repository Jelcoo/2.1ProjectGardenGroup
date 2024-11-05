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
    /// Interaction logic for PwResetReset.xaml
    /// </summary>
    public partial class PwResetReset : UserControl
    {
        UserLogic userLogic = new UserLogic();
        private Employee employee;

        public PwResetReset(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            string password = passwordInput.Password;
            if (password == "")
            {
                errorLabel.Content = "Please enter a valid password!";
                return;
            }

            userLogic.ResetPassword(employee, password);
            userLogic.ClearResetCode(employee);
            ApplicationStore.GetInstance().setLoggedInUser(employee);

            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.svMainContent.Content = new Dashboard();

            Button? dashButton = window.FindName("dashboardButton") as Button;
            Button? ticketsButton = window.FindName("ticketsButton") as Button;
            Button? employeesButton = window.FindName("employeesButton") as Button;
            dashButton.Visibility = Visibility.Visible;
            ticketsButton.Visibility = Visibility.Visible;
            if (employee.role == Role.ServiceDesk)
            {
                employeesButton.Visibility = Visibility.Visible;
            }
        }
    }
}
