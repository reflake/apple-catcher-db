using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class LeaderboardEntry : ILeaderboardEntry
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int Scores { get; set; }
		public int UserId { get; set;  }
		public DateTime DateTime { get; set; }
	}
}