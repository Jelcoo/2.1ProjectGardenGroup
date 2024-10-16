using DAL;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CreateTicketLogic
    {
        CreateTicketDao createTicketDao = new CreateTicketDao();
        public CreateTicketLogic()
        {
        }

        public List<PartialUser> GetEmployees()
        {
            return createTicketDao.Getemployees();
        }
        public void SaveTicket(Ticket ticket)
        {
            createTicketDao.SaveTicket(ticket);
        }
    }
}
