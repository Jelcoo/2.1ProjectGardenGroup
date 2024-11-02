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
        private readonly ObjectId ticketId;  // Use only ObjectId for ticket ID

        public Ticket SelectedTicket { get; set; }
        public ObservableCollection<Comment> LinkedComments { get; set; }

        public TicketDetail(string ticketIdString)
        {
            InitializeComponent();

            // Convert string to ObjectId if possible
            if (ObjectId.TryParse(ticketIdString, out ObjectId parsedTicketId))
            {
                ticketId = parsedTicketId;
                LoadTicketDetails();  // Load details only if ID parsing is successful
            }
            else
            {
                MessageBox.Show("Invalid ticket ID format.");
                this.Close();
            }
        }

        private void LoadTicketDetails()
        {
            // Load ticket and the linked comments using ObjectId
            var (ticket, comments) = ticketLogic.GetTicketWithComments(ticketId);
            SelectedTicket = ticket;
            LinkedComments = new ObservableCollection<Comment>(comments);

            DataContext = this;  // Set DataContext to enable binding in the UI
        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewCommentTextBox.Text))
            {
                MessageBox.Show("Please enter a comment.");
                return;
            }

            // Create a new comment with ObjectId as ticketId
            var newComment = new Comment
            {
                ticketId = ticketId,  // Use ObjectId for ticketId
                message = NewCommentTextBox.Text,
                commentedBy = new PartialUser { Id = "user-id", name = "User Name" },  // Replace with actual user info
                commentedAt = DateTime.UtcNow
            };

            // Add the comment to the ticket
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