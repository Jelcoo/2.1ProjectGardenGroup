using System;

namespace Model
{
    public class Ticket
    {
        public int _id;
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
