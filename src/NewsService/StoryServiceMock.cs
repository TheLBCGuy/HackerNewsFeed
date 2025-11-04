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
        return [1, 2, 3, 4, 5];
    }

    private async Task<Item?> RetrieveStory(int id)
    {
        await Task.CompletedTask;
        return new Item
        {
            Id = id,
            Type = "story",
            Title = $"Mock Story {id}",
            Url = $"https://example.com/story/{id}",
            Deleted = false,
            Dead = false
        };
    }
}
