using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]  // Ensures MongoDB stores _id as a string

        public ObjectId _id { get; set; } 
        public ObjectId ticketId { get; set; }  
        public string message { get; set; } 
        public PartialUser commentedBy { get; set; }  
        public DateTime commentedAt { get; set; } = DateTime.UtcNow;  
    }
}
