using DAL;
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
    }
}
