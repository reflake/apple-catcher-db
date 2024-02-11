using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class Record : IEquatable<Record>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int Scores { get; set; }
		public int Time { get; set; }
		public DateTime DateTime { get; set; }
		public string Nickname { get; set; } = string.Empty;

		public string? UserId { get; set; }


		public bool Equals(Record? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Id == other.Id && Scores == other.Scores && UserId == other.UserId && DateTime.Equals(other.DateTime) && Nickname == other.Nickname;
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Record)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Scores, UserId, DateTime, Nickname);
		}

		public static bool operator ==(Record? left, Record? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Record? left, Record? right)
		{
			return !Equals(left, right);
		}
	}
}