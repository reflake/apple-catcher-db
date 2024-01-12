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
		public string Nickname { get; set; }
		public DateTime Date { get; set; }

		public override string ToString() => $"{Id} - ({Scores}) {Nickname} {Date}";

		public bool Equals(LeaderboardEntry? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Id == other.Id && Scores == other.Scores && Nickname == other.Nickname && Date.Equals(other.Date);
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
			return HashCode.Combine(Id, Scores, Nickname, Date);
		}
	}
}