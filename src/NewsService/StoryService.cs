using DataContract;
using NewsService.Models;
using ServiceContract;
using System.Text.Json;

namespace NewsService;

public class StoryService(
    INewsOptions newsUrlOptions,
    ItemDeserializer itemDeserializer
    ) : IStoryService

{
    private readonly INewsOptions _newsUrlOptions = newsUrlOptions;
    private readonly ItemDeserializer _itemDeserializer = itemDeserializer;

    public async Task<IEnumerable<int>> GetTopStories()
    {
        return await GetLatestStories();
    }

    public async Task<IItem?> GetStory(int id)
    {
        return await RetrieveStory(id);
    }

    private async Task<IEnumerable<int>> GetLatestStories()
    {
        var contents = await ReceiveHttpResponse($"{_newsUrlOptions.BaseUrl}/topstories.json");
        if (null == contents) return [];
        //var ids = JsonSerializer.Deserialize<IEnumerable<int>>(contents);
        var ids = _itemDeserializer.Deserialize<IEnumerable<int>>(contents);
        if (null == ids) return [];
        return ids;
    }

    private async Task<IItem?> RetrieveStory(int id)
    {
        var contents = await ReceiveHttpResponse($"{_newsUrlOptions.BaseUrl}/item/{id}.json");
        if (null == contents) return null;
        //var item = JsonSerializer.Deserialize<Item>(contents, new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true
        //});
        var item = _itemDeserializer.Deserialize<Item>(contents);
        return item;
    }

    private async Task<string> ReceiveHttpResponse(string uri)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
