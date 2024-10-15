using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Model.models
{
	public class Employee : PartialUser
	{
		public Role role { get;set;}

        [BsonRepresentation(BsonType.String)] 

        public string password_hashed { get;set;}
		public string password_salt { get;set;}
	}
}
