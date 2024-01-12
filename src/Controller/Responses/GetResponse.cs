using System.Text.Json.Serialization;

namespace Leaderboard.Responses
{
	public record GetResponse<TEntry> where TEntry : class
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public TEntry Entry { get; set; } = null;
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public TEntry[] Entries { get; set; } = null;
	}
}