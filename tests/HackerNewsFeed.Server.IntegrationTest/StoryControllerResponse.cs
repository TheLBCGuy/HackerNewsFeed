using DataContract;
using System.Text.Json.Serialization;

namespace HackerNewsFeed.Server.IntegrationTest
{
    public class StoryControllerResponse
    {
        [JsonPropertyName("stories")]
        public IEnumerable<Item> Stories { get; init; } = [];

        [JsonPropertyName("total")]
        public int Total { get; init; } = 0;
    }
}
