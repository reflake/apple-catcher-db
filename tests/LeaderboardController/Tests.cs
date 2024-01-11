using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Leaderboard.Responses;
using Entities;

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

		[Test]
		public async Task PostLeaderboardEntry()
		{
			var content = JsonContent.Create(new LeaderboardEntry(0, 200, "Skooma", new DateTime()));
			var response = await _client.PostAsync("Leaderboard", content);

			response.EnsureSuccessStatusCode();

			var putResponse = await response.Content.ReadFromJsonAsync<PutResponse>();

			Assert.AreEqual(putResponse.Id, 1);
		}
	}
}