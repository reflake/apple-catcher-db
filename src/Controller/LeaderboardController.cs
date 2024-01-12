using Database;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Leaderboard
{
	[ApiController]
	[Route("[controller]")]
	public class LeaderboardController : BaseLeaderboardController<LeaderboardEntry>
	{
		public LeaderboardController(AppDbContext<LeaderboardEntry> dbContext) : base(dbContext)
		{
		}
	}
}