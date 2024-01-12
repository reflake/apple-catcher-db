using System.Net.Http.Json;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Database;
using Leaderboard.Responses;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.BaseLeaderboardController
{
	[TestFixture]
	public class GetTests : BaseTests
	{
		[Test]
		public async Task Client_Should_Get_Leaderboard_entries()
		{
			using (var contextProvider = GetContextProvider())
			{
				var context = contextProvider.Context;
				
				var user1 = await PushEntryAsync(context, 20, 1100);
				var user2 = await PushEntryAsync(context, 57, 1200);
				var user3 = await PushEntryAsync(context, 100, 1300);
				var user4 = await PushEntryAsync(context, 157, 1400);
				var user5 = await PushEntryAsync(context, 47, 1500);
			
				var queryBuilder = new QueryBuilder();
				queryBuilder.Add("count", "3");
				queryBuilder.Add("page", "0");
			
				var queryString = queryBuilder.ToQueryString();

				var response = await _client.GetAsync("BaseLeaderboard" + queryString.ToUriComponent());
				var data = await response.Content.ReadFromJsonAsync<GetResponse<TestLeaderboardEntry>>();
				var entries = data.Entries;
				var expected = new[] { user4, user3, user2 };
			
				CollectionAssert.AreEquivalent(expected, entries);
			}
		}

		[Test]
		public async Task Client_Should_Get_Concrete_leaderboard_entry()
		{
			using (var contextProvider = GetContextProvider())
			{
				var context = contextProvider.Context;
				var expectedEntry = await PushEntryAsync(context, 33, 500);

				var response = await _client.GetAsync($"BaseLeaderboard/{expectedEntry.Id}");
				var data = await response.Content.ReadFromJsonAsync<GetResponse<TestLeaderboardEntry>>();
				var actualEntry = data.Entry;
				
				Assert.AreEqual(expectedEntry, actualEntry);
			}
		}
	}
}