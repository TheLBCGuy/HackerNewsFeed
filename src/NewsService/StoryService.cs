using DataContract;
using Microsoft.Extensions.Options;
using ServiceContract;

namespace NewsService;

public class StoryService
    (
        IOptions<NewsOptions> options,
        ItemDeserializer itemDeserializer
    ) : IStoryService

{
    private readonly NewsOptions _newsUrlOptions = options.Value;
    private readonly ItemDeserializer _itemDeserializer = itemDeserializer;

    public async Task<IEnumerable<int>> GetTopStories()
    {
        return await GetLatestStories();
    }

    public async Task<IEnumerable<Item>> GetStories(IEnumerable<int> ids)
    {
        return await RetrieveStories(ids);
    }

    public async Task<Item?> GetStory(int id)
    {
        return await RetrieveStory(id);
    }

    private async Task<IEnumerable<int>> GetLatestStories()
    {
        var contents = await ReceiveHttpResponse($"{_newsUrlOptions.BaseUrl}/topstories.json");
        if (null == contents) return [];
        var ids = _itemDeserializer.Deserialize<IEnumerable<int>>(contents);
        if (null == ids) return [];
        return ids;
    }

    private async Task<IEnumerable<Item>> RetrieveStories(IEnumerable<int> ids)
    {
        var items = new List<Item>();
        foreach (var id in ids)
        {
            var item = await RetrieveStory(id);
            if (item == null) continue;
            items.Add(item);
        }
        return items;
    }

    private async Task<Item?> RetrieveStory(int id)
    {
        var contents = await ReceiveHttpResponse($"{_newsUrlOptions.BaseUrl}/item/{id}.json");
        if (null == contents) return null;
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
