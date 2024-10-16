using Model.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Employee : PartialUser
    {
        public Role role { get; set; }

        [BsonRepresentation(BsonType.String)]

        public string password_hashed { get; set; }
        public string password_salt { get; set; }
    }
}
