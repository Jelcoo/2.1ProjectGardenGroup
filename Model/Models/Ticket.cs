using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Ticket
    {
        public string _id;
        public string title;
        public string description;
        public string status;
        public string priority;
        public Comment[] comments;
        public PartialUser reported_by;
        public PartialUser assigned_to;
        public PartialUser resolved_by;
        public DateTime occurred_at;
        public DateTime resolved_at;
        public DateTime created_at;
    }
}
