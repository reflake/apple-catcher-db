using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class LeaderboardEntry : IEquatable<LeaderboardEntry>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int Scores { get; set; }
		public int UserId { get; set;  }
		public DateTime DateTime { get; set; }

		public bool Equals(LeaderboardEntry? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Id == other.Id && Scores == other.Scores && UserId == other.UserId && DateTime.Equals(other.DateTime);
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((LeaderboardEntry)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Scores, UserId, DateTime);
		}

		public static bool operator ==(LeaderboardEntry? left, LeaderboardEntry? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(LeaderboardEntry? left, LeaderboardEntry? right)
		{
			return !Equals(left, right);
		}
	}
}