using Microsoft.Extensions.Logging;
using ServiceContract;

namespace NewsService;

public class StoryIndexerService(
    IStoryService storyService,
    IIndexer indexer,
    ILogger<StoryIndexerService> logger
    ) : IStoryIndexerService
{
    private readonly IStoryService _storyService = storyService;
    private readonly IIndexer _indexer = indexer;
    private readonly ILogger<StoryIndexerService> _logger = logger;

    public async Task IndexStories()
    {
        _logger.LogInformation("Starting indexing of top stories...");
        var ids = await _storyService.GetTopStories();
        _logger.LogInformation("Retrieved {Count} top stories for indexing.", ids.Count());
        foreach (var id in ids)
        {
            var item = await _storyService.GetStory(id);
            if (item != null)
            {
                _indexer.Add(item);
                _logger.LogInformation("Indexing story with ID: {Id}, Title: {Title}", item.Id, item.Title);
            }
        }
       _logger.LogInformation("Completed indexing of top stories.");
    }

    public async Task<IEnumerable<int>> SearchStories(string term)
    {
        _logger.LogInformation("Searching for stories with term: {Term}", term);
        var resultIds = _indexer.Search(term);
        _logger.LogInformation("Found {Count} stories matching the term: {Term}", resultIds.Count(), term);
        var stories = await _storyService.GetStories(resultIds);
        return resultIds;
    }
}
