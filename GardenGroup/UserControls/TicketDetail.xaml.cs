using Logic;
using Model.Models;
using MongoDB.Bson;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace UI.UserControls
{
    public partial class TicketDetail : Window
    {
        private readonly TicketLogic ticketLogic = new TicketLogic();
        private readonly ObjectId ticketId;  // inplaats van string objectId
        private readonly Employee loggedInUser; 


        public Ticket SelectedTicket { get; set; }
        public ObservableCollection<Comment> LinkedComments { get; set; }

        public TicketDetail(string ticketIdString, Employee loggedInUser)
        {
            InitializeComponent();
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
            if (string.IsNullOrWhiteSpace(NewCommentTextBox.Text))
            {
                MessageBox.Show("Please enter a comment.");
                return;
            }

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
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}