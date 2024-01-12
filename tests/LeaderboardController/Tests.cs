using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Database;
using Leaderboard.Responses;
using Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.LeaderboardController
{
	[TestFixture]
	public class Tests
	{
		private HttpClient _client = null;
		private LeaderboardControllerApiFactory _factory = null;

		[SetUp]
		public async Task Setup()
		{
			_factory = new LeaderboardControllerApiFactory();
			_client = _factory.CreateClient();

			await _factory.InitializeDatabaseAsync();
		}

		[TearDown]
		public async Task TearDown()
		{
			_client.Dispose();
			
			await _factory.DisposeDatabaseAsync();
			await _factory.DisposeAsync();
		}

		private LeaderboardEntry CreateEntry(int scores, string nickname) => new () { Scores = scores, Nickname = nickname };

		private async Task<LeaderboardEntry> PushEntryAsync(AppDbContext context, int scores, string nickname)
		{
			var entry = CreateEntry(scores, nickname);
			
			entry = (await context.LeaderboardEntries.AddAsync(entry)).Entity;
			await context.SaveChangesAsync();

			return entry;
		}

		[Test]
		public async Task PostLeaderboardEntry()
		{
			var content = JsonContent.Create(CreateEntry(200, "Skooma"));
			var response = await _client.PostAsync("Leaderboard", content);

			response.EnsureSuccessStatusCode();

			var putResponse = await response.Content.ReadFromJsonAsync<PutResponse>();

			Assert.AreEqual(putResponse.Id, 1);
		}

		[Test]
		public async Task GetLeaderboardEntries()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				
				var diverEntry = await PushEntryAsync(context, 20, "Diver");
				var rabbitEntry = await PushEntryAsync(context, 57, "Rabbit");
				var nikolasEntry = await PushEntryAsync(context, 100, "Nikolas");
				var urielEntry = await PushEntryAsync(context, 157, "Uriel");
				var zombieEntry = await PushEntryAsync(context, 47, "Zombie");
			
				var queryBuilder = new QueryBuilder();
				queryBuilder.Add("count", "3");
				queryBuilder.Add("page", "0");
			
				var queryString = queryBuilder.ToQueryString();

				var response = await _client.GetAsync($"Leaderboard/{queryString.ToUriComponent()}");
				var data = await response.Content.ReadFromJsonAsync<GetResponse>();
				var entries = data.Entries;
				var expected = new[] { urielEntry, nikolasEntry, rabbitEntry };
			
				CollectionAssert.AreEquivalent(expected, entries);
			}
		}
	}
}