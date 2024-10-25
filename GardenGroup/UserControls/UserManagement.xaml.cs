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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : UserControl
    {
        private readonly UserLogic userLogic;
        private List<Employee> allUsers;

        public UserManagement()
        {
            InitializeComponent();
            userLogic = new UserLogic();
            LoadUsers();
        }

        private void LoadUsers()
        {
            allUsers = userLogic.GetAllUsers();
            dataGridUsers.ItemsSource = allUsers;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedItem is Employee selectedUser)
            {
                EditUser editUserControl = new EditUser(selectedUser);

                this.Content = editUserControl;


                LoadUsers();
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedItem is Employee selectedUser)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedUser.name}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    userLogic.DeleteUser(selectedUser.Id);
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void btnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee createEmployeeControl = new CreateEmployee();
            this.Content = createEmployeeControl;

            LoadUsers();
        }
    }
}
