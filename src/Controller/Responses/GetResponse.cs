using System.Text.Json.Serialization;
using Entities;

namespace Leaderboard.Responses
{
	public record GetResponse
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Record Entry { get; set; } = null;
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Record[] Entries { get; set; } = null;
	}
}