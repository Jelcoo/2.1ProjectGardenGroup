using MongoDB.Bson;

namespace Model
{
	public class PartialUser
	{
		public ObjectId Id;
		public string Name;
		public string Email;
		public string PhoneNumber;
	}
}
