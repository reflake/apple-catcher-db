﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	[PrimaryKey("Id")]
	public class UserEntry : IdentityUser
	{
		public DateTime CreationDate { get; set; }
	}
}