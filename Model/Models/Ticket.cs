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
        public string Title;
        public string Description;
        public string Status;
        public string Priority;
        public Comment[] Comments;
        public PartialUser ReportedBy;
        public PartialUser AssignedTo;
        public PartialUser ResolvedBy;
        public DateTime OccurredAt;
        public DateTime ResolvedAt;
        public DateTime CreatedAt;
    }
}
