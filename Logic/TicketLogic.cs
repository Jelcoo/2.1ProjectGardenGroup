using DAL;
using Model.Enums;
using Model.Models;
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
        public TicketLogic()
        {
        }

        public List<Ticket> GetTicketsEmployees()
        {
            return ticketDao.GetTicketsEmployees();
        }
        public List<Ticket> GetTicketsByStatus(Status_Enum status)
        {
            return ticketDao.GetTicketsByStatus(status);
        }
        public List<Ticket> SearchTickets(string searchQuery)
        {
            return ticketDao.SearchTickets(searchQuery);
        }
        public void ChangeTicketStatus(string ticketId, Status_Enum status)
        {
            ticketDao.ChangeTicketStatus(ticketId, status);
        }
    }
}
