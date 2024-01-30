using System.Text.Json.Serialization;
using Entities;

namespace Leaderboard.Responses
{
	public record GetResponse
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public LeaderboardEntry Entry { get; set; } = null;
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public LeaderboardEntry[] Entries { get; set; } = null;
	}
}