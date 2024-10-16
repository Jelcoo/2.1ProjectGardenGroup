using Model.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CreateTicketDao : DAO
    {
        public CreateTicketDao() : base()
        {

        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> tickets = Db.GetCollection<Ticket>("tickets")
                .Aggregate()
                .ToList();

            return tickets;
        }
        public List<PartialUser> Getemployees()
        {
            List<PartialUser> employees = Db.GetCollection<PartialUser>("employees")
                          .Aggregate()
                          .Project(e => new PartialUser
                          {
                              Id = e.Id,
                              name = e.name,
                              email = e.email,
                              phone_number = e.phone_number
                          })
                          .ToList();

            return employees;
        }

        public void SaveTicket(Ticket ticket)
        {
            IMongoCollection<Ticket> ticketCollection = Db.GetCollection<Ticket>("tickets");
            ticketCollection.InsertOne(ticket);
        }
    }
}
