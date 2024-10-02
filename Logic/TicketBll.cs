using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class TicketBll
	{
		TicketDao ticketDao = new TicketDao();
		public TicketBll()
		{
		}

		public List<Ticket> GetTicketsEmployees()
		{
			return ticketDao.GetTicketsEmployees();
		}
	}
}
