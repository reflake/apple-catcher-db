using System.Net.Http;
using System.Threading.Tasks;
using Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.BaseLeaderboardController
{
	public abstract class BaseTests
	{
		protected HttpClient _client = null;
		protected LeaderboardControllerApiFactory _factory = null;
		
		[SetUp]
		public async Task BaseSetup()
		{
			_factory = new LeaderboardControllerApiFactory();
			_client = _factory.CreateClient();

			await _factory.InitializeDatabaseAsync();
		}

		[TearDown]
		public async Task BaseTearDown()
		{
			_client.Dispose();
			
			await _factory.DisposeDatabaseAsync();
			await _factory.DisposeAsync();
		}
		protected TestLeaderboardEntry CreateEntry(int scores, int uid) => new () { Scores = scores, UserId = uid };

		protected async Task<TestLeaderboardEntry> PushEntryAsync(AppDbContext<TestLeaderboardEntry> context, int scores, int uid)
		{
			var entry = CreateEntry(scores, uid);
			
			entry = (await context.LeaderboardEntries.AddAsync(entry)).Entity;
			await context.SaveChangesAsync();

			return entry;
		}

		protected ContextProvider<TestLeaderboardEntry> GetContextProvider()
		{
			var scope = _factory.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<AppDbContext<TestLeaderboardEntry>>();

			return new ContextProvider<TestLeaderboardEntry>(scope, context);
		}
	}
}