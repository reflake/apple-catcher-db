﻿using System.Linq;
using System.Threading.Tasks;
using Database;
using Leaderboard.Responses;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetLeaderboardEntry(int id)
		{
			var entry = await _dbContext.LeaderboardEntries.FindAsync(id);

			return Ok(new GetResponse { Entry = entry });
		}

		[HttpGet]
		public async Task<IActionResult> GetLeaderboardEntries([FromQuery] int count, [FromQuery] int page)
		{
			var entries = await _dbContext.LeaderboardEntries
				.OrderByDescending(e => e.Scores)
				.Skip(count * page)
				.Take(count)
				.ToArrayAsync();

			return Ok(new GetResponse {Entries = entries});
		}

		[HttpPost]
		public async Task<IActionResult> PostLeaderboardEntry(Record newEntry)
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
					return Ok(new PostResponse
					{
						Faulted = true,
						ErrorMessage = "The posted score must be higher than the user's previously highest posted score"
					});
				}
			}

			var result = await _dbContext.LeaderboardEntries.AddAsync(newEntry);

			await _dbContext.SaveChangesAsync();

			return Ok(new PostResponse
			{
				Id = result.Entity.Id
			} );
		}
	}
}