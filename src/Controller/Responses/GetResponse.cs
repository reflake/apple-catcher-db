using Entities;

namespace Leaderboard.Responses
{
	public record GetResponse(LeaderboardEntry[] Entries);
}