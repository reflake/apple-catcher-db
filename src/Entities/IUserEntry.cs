namespace Entities
{
	public interface IUserEntry
	{
		int Id { get; set; }
		string Username { get; set; }
		string Password { get; set; }
	}
}