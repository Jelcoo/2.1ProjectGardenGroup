using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL
{
	public class TicketDao : DAO
	{
		public TicketDao() : base()
		{

		}

		public List<Ticket> GetTicketsEmployees()
		{
			// Get the "tickets" with a "Priority" of "High"
			List<Ticket> tickets = Db.GetCollection<Ticket>("tickets")
				.Aggregate()
				.Match(e => e.Priority == "High")
				.ToList();

			return tickets;
		}
	}
}
