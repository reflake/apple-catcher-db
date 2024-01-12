using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Tests
{
	public class LeaderboardControllerApiFactory : WebApplicationFactory<Program>
	{
		private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
			.WithImage("postgres:15-alpine")
			.WithPortBinding(5432, true)
			.Build();
		
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			var assembly = typeof(LeaderboardControllerApiFactory).Assembly;
			
			builder.ConfigureServices(services =>
			{
				// Remove default database context
				var dbContextDescriptor = services.SingleOrDefault(
					d => d.ServiceType ==
					     typeof(DbContextOptions<AppDbContext<TestLeaderboardEntry>>));

				services.Remove(dbContextDescriptor);

				var dbConnectionDescriptor = services.SingleOrDefault(
					d => d.ServiceType ==
					     typeof(DbConnection));

				services.Remove(dbConnectionDescriptor);
				
				// Create open NpgsqlConnection
				services.AddSingleton<DbConnection>(container =>
				{
					var connection = new NpgsqlConnection(_postgres.GetConnectionString());
					connection.Open();
					
					return connection;
				});
				
				services.AddDbContext<AppDbContext<TestLeaderboardEntry>>((container, options) =>
				{
					var connection = container.GetRequiredService<DbConnection>();
					options.UseNpgsql(connection);
				});

				services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
			});
			
			builder.UseEnvironment("Development");
			
			base.ConfigureWebHost(builder);
		}

		public Task InitializeDatabaseAsync() => _postgres.StartAsync();

		public Task DisposeDatabaseAsync() => _postgres.DisposeAsync().AsTask();
	}
}