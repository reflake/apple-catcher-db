using System.Threading.Tasks;
using Database;
using Leaderboard.Responses;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Leaderboard
{
	[ApiController]
	[Route("[controller]")]
	public class LeaderboardController : ControllerBase
	{
		private readonly AppDbContext _dbContext;

		public LeaderboardController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpPost]
		public async Task<IActionResult> PostLeaderboardEntry(LeaderboardEntry entry)
		{
			var result = await _dbContext.LeaderboardEntries.AddAsync(entry);
			
			await _dbContext.SaveChangesAsync();

			return Ok(new PutResponse(result.Entity.Id));
		}
	}
}