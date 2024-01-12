using Database;
using Leaderboard;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
	[ApiController]
	[Route("BaseLeaderboard")]
	public class TestBaseLeaderboardController : BaseLeaderboardController<TestLeaderboardEntry>
	{
		public TestBaseLeaderboardController(AppDbContext<TestLeaderboardEntry> dbContext) : base(dbContext)
		{
		}
	}
}