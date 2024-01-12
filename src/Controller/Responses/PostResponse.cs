namespace Leaderboard.Responses
{
	public record PostResponse
	{
		public int? Id { get; set; }
		public bool Faulted { get; set; } = false;
		public string? ErrorMessage { get; set; }
	}
}