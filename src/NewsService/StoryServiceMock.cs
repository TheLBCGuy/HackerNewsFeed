using DataContract;
using ServiceContract;

namespace NewsService;

public class StoryServiceMock : IStoryService
{
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
        await Task.CompletedTask;

        return Enumerable.Range(1, 500).ToList();
    }

    private async Task<Item?> RetrieveStory(int id)
    {
        await Task.CompletedTask;
        return new Item
        {
            Id = id,
            Type = "story",
            By = $"Author {id}",
            Title = $"Mock Story {id}",
            Url = $"https://example.com/story/{id}",
            Deleted = false,
            Dead = false
        };
    }
}
