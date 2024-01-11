using System;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public record LeaderboardEntry(int Id, int Scores, string Nickname, DateTime Date);
}