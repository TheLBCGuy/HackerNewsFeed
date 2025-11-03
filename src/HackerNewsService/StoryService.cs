using NewsService.Models;

namespace NewsService;

public class StoryService() 
{
    public async Task<IEnumerable<Item>> GetTopStoriesAsync()
    {
        return await GetLatestStories();
    }

    private async Task<IEnumerable<Item>> GetLatestStories()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://hacker-news.firebaseio.com/v0/topstories.json"),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
        }
        return [];
    }
}
