using System.Net.Http.Json;
using System.Threading.Tasks;
using Database;
using Leaderboard.Responses;
using Microsoft.Extensions.DependencyInjection;

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

		[Test]
		public async Task Client_Shouldnt_Post_Entry_with_low_score()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext<TestLeaderboardEntry>>();

				PushEntryAsync(context, 50, 1);

				var content = JsonContent.Create(CreateEntry(10, 1));
				var response = await _client.PostAsync("BaseLeaderboard", content);

				response.EnsureSuccessStatusCode();

				var putResponse = await response.Content.ReadFromJsonAsync<PutResponse>();
				
				Assert.True(putResponse.Faulted);
				Assert.AreEqual(putResponse.ErrorMessage, "Posted score is lower than the highest posted score by user");
			}
		}
	}
}