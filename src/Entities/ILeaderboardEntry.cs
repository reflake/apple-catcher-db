namespace Entities
{
	public interface ILeaderboardEntry
	{
		int Id { get; set; }
		int Scores { get; set; }
		int UserId { get; set; }
	}
}