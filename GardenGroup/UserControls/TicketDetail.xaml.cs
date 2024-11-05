using DAL;
using Logic;
using Model.Enums;
using Model.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls
{
    public partial class TicketDetail : Window
    {
        private readonly TicketLogic ticketLogic = new TicketLogic();
        private readonly ObjectId ticketId;  // inplaats van string objectId
        private readonly Employee loggedInUser;
        public List<PartialUser> employees;
        private PartialUser selectedEmployee;



        public Ticket SelectedTicket { get; set; }
        public ObservableCollection<Comment> LinkedComments { get; set; }

        public TicketDetail(string ticketIdString, Employee loggedInUser)
        {
            InitializeComponent();
            EnableAssignToPerson(loggedInUser);
            this.loggedInUser = loggedInUser; // ingelogde gebruiker opslaan


            if (ObjectId.TryParse(ticketIdString, out ObjectId parsedTicketId))
            {
                ticketId = parsedTicketId;
                LoadTicketDetails();
            }
            else
            {
                MessageBox.Show("Invalid ticket ID format.");
                this.Close();
            }
        }

        private void LoadTicketDetails()
        {
            // laad ticket en de gekoppelde comment
            var (ticket, comments) = ticketLogic.GetTicketWithComments(ticketId);
            SelectedTicket = ticket;
            LinkedComments = new ObservableCollection<Comment>(comments);

            DataContext = this;
        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewCommentTextBox.Text) && selectedEmployee == null)
            {
                MessageBox.Show("Please enter a comment.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(NewCommentTextBox.Text) && selectedEmployee != null)
            {
                UpdateAssignTo();
            }
            else
            {
                // nieuwe comment aanmaken met ticketid/objectid
                var newComment = new Comment
                {
                    ticketId = ticketId,
                    message = NewCommentTextBox.Text,
                    commentedBy = new PartialUser { _id = loggedInUser._id, name = loggedInUser.name },
                    commentedAt = DateTime.UtcNow
                };

                // Voeg de opmerking toe aan het ticket
                ticketLogic.AddCommentToTicket(ticketId, newComment);
                LinkedComments.Add(newComment);
                NewCommentTextBox.Clear();
                if (selectedEmployee != null)
                {
                    UpdateAssignTo();
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnableAssignToPerson(Employee loggedInUser)
        {
            if (loggedInUser.role == Role.ServiceDesk)
            {
                employees = ticketLogic.GetEmployees();
                labelPersonInCharge.Visibility = Visibility.Visible;
                ComboBoxEmployee.Visibility = Visibility.Visible;
                ComboBoxEmployee.ItemsSource = employees;
                ComboBoxEmployee.DisplayMemberPath = "name";
                ComboBoxEmployee.SelectedValuePath = "_id";

            }
        }

        private void UpdateAssignTo()
        {
            if (selectedEmployee != null)
            {
                ticketLogic.AssignTicketToEmployee(selectedEmployee, SelectedTicket);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the entire selected employee object from combobox
            selectedEmployee = ComboBoxEmployee.SelectedItem as PartialUser;

        }



    }
}