using Logic;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : UserControl
    {
        private readonly UserLogic userLogic;
        private readonly Employee selectedUser;

        public EditUser(Employee user)
        {
            InitializeComponent();
            userLogic = new UserLogic();
            selectedUser = user;

            LoadUserData();
        }

        private void LoadUserData()
        {
            txtBoxEditName.Text = selectedUser.name;
            txtBoxEditEmail.Text = selectedUser.email;
            txtBoxEditPhonenumber.Text = selectedUser.phone_number;
            cmBoxEditRole.ItemsSource = Enum.GetValues(typeof(Role));
            cmBoxEditRole.SelectedItem = selectedUser.role;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            selectedUser.name = txtBoxEditName.Text;
            selectedUser.email = txtBoxEditEmail.Text;
            selectedUser.phone_number = txtBoxEditPhonenumber.Text;
            selectedUser.role = (Role)cmBoxEditRole.SelectedItem;

            userLogic.UpdateUser(selectedUser.Id, selectedUser.name, selectedUser.email, selectedUser.phone_number, selectedUser.role);

            MessageBox.Show("User updated successfully!");

            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            if (parentWindow != null)
            {
                parentWindow.Content = new UserManagement();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            parentWindow.Content = new UserManagement();
        }
    }
}
