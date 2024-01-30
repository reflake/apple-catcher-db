using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class UserEntry : IdentityUser
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public DateTime CreationDate { get; set; }
	}
}