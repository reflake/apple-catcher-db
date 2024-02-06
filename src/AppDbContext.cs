using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
	public class AppDbContext : IdentityDbContext<UserEntry>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		
		public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			/*builder.Entity<UserEntry>()
				.HasMany(e => e.LeaderboardEntries)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.HasPrincipalKey(e => e.Id);*/
		}
	}
}