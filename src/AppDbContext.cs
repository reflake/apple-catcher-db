using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
	public class AppDbContext<TEntry> : IdentityDbContext
		where TEntry : class
	{
		public AppDbContext(DbContextOptions<AppDbContext<TEntry>> options) : base(options)
		{
			Database.EnsureCreated();
		}
		
		public DbSet<TEntry> LeaderboardEntries { get; set; }
	}
}