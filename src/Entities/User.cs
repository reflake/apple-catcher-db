using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class User : IdentityUser
	{
		public DateTime CreationDate { get; set; }
		public ICollection<Record> LeaderboardEntries { get; }
	}
}