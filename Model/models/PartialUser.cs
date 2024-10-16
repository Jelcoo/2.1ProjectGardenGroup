using MongoDB.Bson;

namespace Model
{
	public class PartialUser
	{
		public string Id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string phone_number { get; set; }
	}
}
