using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
	[PrimaryKey("Id")]
	public class TestLeaderboardEntry : ILeaderboardEntry, IEquatable<TestLeaderboardEntry>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int Scores { get; set; }
		public int UserId { get; set; }

		public bool Equals(TestLeaderboardEntry? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Id == other.Id && Scores == other.Scores && UserId == other.UserId;
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TestLeaderboardEntry)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Scores, UserId);
		}

		public override string ToString() => $"(Entry {Id} {Scores} {UserId})";
	}
}