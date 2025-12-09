using DataContract;
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using ServiceContract;

namespace Caching;

public class StoryServiceCache
    (
        IStoryService storyService,
        IEasyCachingProvider provider,
        ILogger<StoryServiceCache> logger
    ) : IStoryService

{
    private readonly IStoryService _storyService = storyService;
    private readonly IEasyCachingProvider _provider = provider;
    private readonly ILogger<StoryServiceCache> _logger = logger;
    private readonly int expirationOfTopStoriesInHours = 1;
    private readonly int expirationOfStoryInHours = 48;

    public async Task<IEnumerable<int>> GetTopStories()
    {
        var key = "all-top-stories";
        var cacheResult = await _provider.GetAsync<IEnumerable<int>>(key);
        if (cacheResult.HasValue)
        {
            _logger.LogInformation("Cache hit for Top Stories ({Count})", cacheResult.Value.Count().ToString());
            return cacheResult.Value;
        }
        else
        {
            var model = await _storyService.GetTopStories();
            await _provider.SetAsync(key, model, TimeSpan.FromHours(expirationOfTopStoriesInHours));
            return model;
        }
    }

    public async Task<Item?> GetStory(int id)
    {
        var key = $"{id}-story";
        var cacheResult = await _provider.GetAsync<Item>(key);
        if (cacheResult.HasValue)
        {
            _logger.LogInformation("Cache hit for GetStory {id}", cacheResult.Value.Id.ToString());
            return cacheResult.Value;
        }
        else
        {
            var model = await _storyService.GetStory(id);
            if (model == null) return null;
            await _provider.SetAsync(key, model, TimeSpan.FromHours(expirationOfStoryInHours));
            return model;
        }
    }
}
