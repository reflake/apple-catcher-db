using System.Linq;
using System.Threading.Tasks;
using Database;
using Leaderboard.Responses;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Leaderboard
{
	public abstract class BaseLeaderboardController<TEntry> : ControllerBase
	
		where TEntry : class, ILeaderboardEntry
	{
		private readonly AppDbContext<TEntry> _dbContext;

		public BaseLeaderboardController(AppDbContext<TEntry> dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetLeaderboardEntries(
			[FromQuery(Name = "count")] int count,
			[FromQuery(Name = "page")]  int page)
		{
			var entries = await _dbContext.LeaderboardEntries
				.OrderByDescending(e => e.Scores)
				.Skip(count * page)
				.Take(count)
				.ToArrayAsync();

			return Ok(new GetResponse<TEntry>(entries));
		}

		[HttpPost]
		public async Task<IActionResult> PostLeaderboardEntry(TEntry entry)
		{
			var result = await _dbContext.LeaderboardEntries.AddAsync(entry);
			
			await _dbContext.SaveChangesAsync();

			return Ok( new PutResponse { Id = result.Entity.Id } );
		}
	}
}