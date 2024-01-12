using System.Text.Json.Serialization;

namespace Leaderboard.Responses
{
	public record PostResponse
	{
		public bool Faulted { get; set; } = false;
		
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public int? Id { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? ErrorMessage { get; set; }
	}
}