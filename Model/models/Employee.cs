namespace Model.models
{
	public class Employee : PartialUser
	{
		public Role role;
		public string password_hashed;
		public string password_salt;
	}
}
