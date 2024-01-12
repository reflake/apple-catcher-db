namespace Entities
{
	public interface ILeaderboardEntry
	{
		int Id { get; }
		int Scores { get; }
		int UserId { get; }
	}
}