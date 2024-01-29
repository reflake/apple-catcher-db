using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class UserEntry : IUserEntry
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public DateTime CreationDate { get; set; }
	}
}