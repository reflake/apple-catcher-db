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
		public async Task<IActionResult> PostLeaderboardEntry(TEntry newEntry)
		{
			// Take a look if there's already entry of same user with higher score
			if (await _dbContext.LeaderboardEntries.AnyAsync(dbEntry => dbEntry.UserId == newEntry.UserId))
			{
				var highestScoreEntry =
					await _dbContext.LeaderboardEntries
						.Where(dbEntry => dbEntry.UserId == newEntry.UserId)
						.Select(dbEntry => dbEntry.Scores)
						.MaxAsync();

				if (newEntry.Scores <= highestScoreEntry)
				{
					return Ok(new PutResponse
					{
						Faulted = true,
						ErrorMessage = "Posted score is lower than the highest posted score by user"
					});
				}
			}

			var result = await _dbContext.LeaderboardEntries.AddAsync(newEntry);

			await _dbContext.SaveChangesAsync();

			return Ok(new PutResponse
			{
				Id = result.Entity.Id
			} );
	}
	}
}