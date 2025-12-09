using DataContract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceContract;

namespace NewsService;

public class StoryService
    (
        IOptions<NewsOptions> options,
        ItemDeserializer itemDeserializer,
        ILogger<StoryService> logger
    ) : IStoryService

{
    private readonly NewsOptions _newsUrlOptions = options.Value;
    private readonly ItemDeserializer _itemDeserializer = itemDeserializer;
    private readonly ILogger<StoryService> _logger = logger;

    public async Task<IEnumerable<int>> GetTopStories()
    {
        return await GetLatestStories();
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
        _logger.LogInformation("Fetched top {Count} stories", ids.Count().ToString());
        return ids;
    }

    private async Task<Item?> RetrieveStory(int id)
    {
        var contents = await ReceiveHttpResponse($"{_newsUrlOptions.BaseUrl}/item/{id}.json");
        if (null == contents) return null;
        var item = _itemDeserializer.Deserialize<Item>(contents);
        _logger.LogInformation("Fetched story {id}", id.ToString());
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
