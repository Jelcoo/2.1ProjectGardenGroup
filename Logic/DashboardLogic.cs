//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using CommunityToolkit.Mvvm.ComponentModel;
//using System.Threading.Tasks;
//using LiveChartsCore.SkiaSharpView.Extensions;
//using LiveChartsCore;
using DAL;
//using Model;
//using System.Collections.Generic;
//using SkiaSharp;
//using HarfBuzzSharp;
//using LiveChartsCore.SkiaSharpView;
//using LiveChartsCore.SkiaSharpView.Painting;
using Model.Models;

namespace Logic
{
    public partial class DashboardLogic// : ObservableObject
    {
        private const string OPEN_TICKET_TITLE = "Open Tickets";
        private const string CLOSED_TICKET_TITLE = "Closed Tickets";
        private const string PAST_DEADLINE_TITLE = "Tickets Past Deadline";
        public int GaugeTotalPastDeadlineTickets { get; set; }
        public int GaugeTotalOpenTickets { get; set; }

        private TicketDao ticketDao;

        //public ISeriesView[] ClosedAndOpenTicketsSeries { get; set; }
        //public ISeriesView[] TicketsPastDeadlineSeries { get; set; }

        //this Constructors initialize the methods that Read the data from the MongoDB and the passes the data to the Methods that are needed to fill the charts
        public DashboardLogic()
        {
            ticketDao = new TicketDao();
            List<TicketsCount> openTicketsCounts = ticketDao.GetTicketsCountByStatus("open");
            List<TicketsCount> closedTicketsCounts = ticketDao.GetTicketsCountByStatus("closed");
            List<TicketsCount> ticketsPastDeadline = ticketDao.GetTicketsPastDeadlineCount();

            FillPieChartOpenAndClosedTickets(openTicketsCounts, closedTicketsCounts);
            FillPieChartPastDealineTickets(ticketsPastDeadline, closedTicketsCounts);
        }

        //this metod fills the data needed for the PieChart/Gauge for the Open Tickets
        private void FillPieChartOpenAndClosedTickets(List<TicketsCount> openTicketsCounts, List<TicketsCount> closedTicketsCounts)
        {
            //GaugeTotalOpenTickets = closedTicketsCounts[0].count;
            //int openTicketsCount = 0;
            //if (openTicketsCounts.Count > 0)
            //{
            //    openTicketsCount = openTicketsCounts.Count;
            //}

            //ClosedAndOpenTicketsSeries = new GaugeBuilder()
            //.WithLabelsSize(50)  // Set label size
            //.WithInnerRadius(50) // Set inner radius
            //.WithBackgroundInnerRadius(50) // Set background inner radius
            //.AddValue(openTicketsCount, OPEN_TICKET_TITLE, SKColors.Red)
            //.BuildSeries()
            //.ToArray(); // Convert the result to an array to match the ISeries[] type
        }


        //this metod fills the data needed for the PieChart/Gauge for the Tickets Past the Deadline
        private void FillPieChartPastDealineTickets(List<TicketsCount> ticketsPastDeadline, List<TicketsCount> closedTicketsCounts)
        {
            //GaugeTotalPastDeadlineTickets = closedTicketsCounts[0].count;
            //int count = 0;
            //if (ticketsPastDeadline.Count > 0)
            //{
            //    count = ticketsPastDeadline.Count;
            //}

            //TicketsPastDeadlineSeries = new GaugeBuilder()
            //.WithLabelsSize(50)  // Set label size
            //.WithInnerRadius(50) // Set inner radius
            //.WithBackgroundInnerRadius(50) // Set background inner radius
            //.AddValue(count, PAST_DEADLINE_TITLE, SKColors.Red)
            //.BuildSeries()
            //.ToArray(); // Convert the result to an array to match the ISeries[] type
        }

    }
}