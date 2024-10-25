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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PartialUser newPartialUser = new PartialUser();
            newPartialUser.Id = selectedUser.Id;
            newPartialUser.name = txtBoxEditName.Text;
            newPartialUser.email = txtBoxEditEmail.Text;
            newPartialUser.phone_number = txtBoxEditPhonenumber.Text;
            Role role = (Role)cmBoxEditRole.SelectedItem;

            userLogic.UpdateUser(newPartialUser, role);

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
