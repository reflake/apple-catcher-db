using Entities;

namespace Leaderboard.Responses
{
	public record GetResponse<TEntry>(TEntry[] Entries);
}