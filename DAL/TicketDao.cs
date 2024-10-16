using Model.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<TicketsCount> GetTicketsCountByStatus(string status)
        {
            try
            {
                List<TicketsCount> tickets = Db.GetCollection<Ticket>("tickets")
                    .Aggregate()
                    .Match(e => e.Status == status)
                    .Group(
                        g => $"{status} tickets",
                        g => new TicketsCount
                        {
                            _id = g.Key,
                            count = g.Count()
                        }).ToList();

                return tickets;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<TicketsCount> GetTicketsPastDeadlineCount()
        {
            List<TicketsCount> tickets = Db.GetCollection<Ticket>("tickets")
                .Aggregate()
                .Match(e => e.Status == "open"
                && e.CreatedAt < DateTime.UtcNow)
                .Group(g => "Tickets Past Deadline",
                g => new TicketsCount
                {
                    _id = g.Key,
                    count = g.Count(),
                }).ToList();

            return tickets;

        }
    }
}
