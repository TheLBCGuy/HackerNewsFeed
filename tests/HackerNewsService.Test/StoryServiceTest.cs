using Xunit;

namespace NewsService.Test;

public class StoryServiceTest
{
    [Fact]
    public async Task TestGetTopStories()
    {
        var options = new NewsOptions
        {
            BaseUrl = "https://hacker-news.firebaseio.com/v0"
        };
        var service = new StoryService(options, ItemDeserializer.Instance);

        var ids = await service.GetTopStories();
        Assert.NotNull(ids);
        Assert.Equal(500, ids.Count());
    }

    [Fact]
    public async Task TestGetStory()
    {
        var options = new NewsOptions
        {
            BaseUrl = "https://hacker-news.firebaseio.com/v0"
        };
        var service = new StoryService(options, ItemDeserializer.Instance);

        var item = await service.GetStory(45691127);

        Assert.NotNull(item);
        Assert.Equal(45691127, item.Id);
        Assert.InRange(item.DateTime, new DateTime(2025,1,1), new DateTime(2025,12,31));
    }
}
