namespace Model.models
{
	public class Employee : PartialUser
	{
		public Role Role;
		public string PasswordHash;
		public string PasswordSalt;
	}
}
