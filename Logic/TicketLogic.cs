using DAL;
using Model.Enums;
using Model.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class TicketLogic
	{
		TicketDao ticketDao = new TicketDao();
		private readonly CommentLogic commentLogic = new CommentLogic();

		public TicketLogic()
		{
		}

		public void SaveTicket(Ticket ticket)
		{
			ticketDao.SaveTicket(ticket);
		}

		public List<Ticket> GetTicketsEmployees(PartialUser loggedInUser)
		{
			return ticketDao.GetTicketsEmployees(loggedInUser);
		}

		public List<Ticket> SearchTickets(string searchQuery, PartialUser loggedInUser)
		{
			return ticketDao.SearchTickets(searchQuery, loggedInUser);
		}

		public void AddCommentToTicket(ObjectId ticketId, Comment comment)
		{
			commentLogic.AddComment(comment);
			ticketDao.AddCommentIdToTicket(ticketId, comment._id);
		}

		//methode om de ticket en bijbehorende comment op te halen
		public (Ticket, List<Comment>) GetTicketWithComments(ObjectId ticketId)
		{
			var ticket = ticketDao.GetTicketById(ticketId);
			var comments = commentLogic.GetCommentsForTicket(ticketId);
			return (ticket, comments);
		}

		public void AssignTicketToEmployee(PartialUser employee, Ticket ticket)
		{
			ticketDao.UpdateAssigneTo(ticket, employee);
		}
	}
}
