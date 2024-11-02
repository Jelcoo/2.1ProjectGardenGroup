using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Ticket
    {
        [BsonId]
        public ObjectId _id {  get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string priority { get; set; }

        [BsonRepresentation(BsonType.String)]

        public List<ObjectId> commentIds { get; set; } = new List<ObjectId>();  // id opslaan van de comment alleen

        public PartialUser reported_by { get; set; }
        public PartialUser assigned_to { get; set; }
        public PartialUser resolved_by { get; set; }
        public DateTime occurred_at { get; set; }
        public DateTime resolved_at {  get; set; }
        public DateTime created_at { get; set; }
    }
}
