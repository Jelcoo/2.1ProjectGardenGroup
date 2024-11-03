using Model.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using Tools;

namespace UI.UserControls
{
    /// <summary>
    /// Interaction logic for PwResetCode.xaml
    /// </summary>
    public partial class PwResetCode : UserControl
    {
        private Employee employee;

        public PwResetCode(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }

        private void verifyButton_Click(object sender, RoutedEventArgs e)
        {
            string code = codeInput.Text;

            if (code == "" || employee == null)
            {
                errorLabel.Content = "A incorrect code has been provided. Please try again.";
                return;
            }

            string inputHashed = PasswordTools.HashPassword(employee.password_reset_salt, code);
            if (inputHashed != employee.password_reset_hashed)
            {
                errorLabel.Content = "A incorrect code has been provided. Please try again.";
                return;
            }

            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.svMainContent.Content = new PwResetReset(employee);
        }
    }
}
