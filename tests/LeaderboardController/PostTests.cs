using System.Net.Http.Json;
using System.Threading.Tasks;
using Database;
using Leaderboard.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.LeaderboardController
{
	public class PostTests : BaseTests
	{
		[Test]
		public async Task Client_Should_Post_One_entry()
		{
			var content = JsonContent.Create(CreateEntry(200, "100"));
			var response = await _client.PostAsync("Leaderboard", content);

			response.EnsureSuccessStatusCode();

			var putResponse = await response.Content.ReadFromJsonAsync<PostResponse>();

			Assert.AreEqual(putResponse.Id, 1);
		}

		[Test]
		public async Task Client_Shouldnt_Post_Entry_with_low_score()
		{
			using (var contextProvider = GetContextProvider())
			{
				var context = contextProvider.Context;

				PushEntryAsync(context, 50, "1");

				var content = JsonContent.Create(CreateEntry(10, "1"));
				var response = await _client.PostAsync("Leaderboard", content);

				response.EnsureSuccessStatusCode();

				var putResponse = await response.Content.ReadFromJsonAsync<PostResponse>();
				
				Assert.True(putResponse.Faulted);
				Assert.AreEqual(putResponse.ErrorMessage, "The posted score must be higher than the user's previously highest posted score");
			}
		}
	}
}