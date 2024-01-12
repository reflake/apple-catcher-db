using System.Net.Http.Json;
using System.Threading.Tasks;
using Leaderboard.Responses;

namespace Tests.BaseLeaderboardController
{
	public class PostTests : BaseTests
	{
		[Test]
		public async Task Client_Should_Post_One_entry()
		{
			var content = JsonContent.Create(CreateEntry(200, 100));
			var response = await _client.PostAsync("BaseLeaderboard", content);

			response.EnsureSuccessStatusCode();

			var putResponse = await response.Content.ReadFromJsonAsync<PutResponse>();

			Assert.AreEqual(putResponse.Id, 1);
		}
	}
}