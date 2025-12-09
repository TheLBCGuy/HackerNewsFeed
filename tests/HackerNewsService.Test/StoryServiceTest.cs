using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace NewsService.Test;

public class StoryServiceTest
{
    private IOptions<NewsOptions> _options;
    private StoryService _service;

    public StoryServiceTest()
    {
        var newsOptions = new NewsOptions
        {
            BaseUrl = "https://hacker-news.firebaseio.com/v0"
        };
        var  _logger = Mock.Of<ILogger<StoryService>>();
        _options = Options.Create(newsOptions);
        _service = new StoryService(_options, ItemDeserializer.Instance, _logger);
    }

    [Fact]
    public async Task TestGetTopStories()
    {
        var ids = await _service.GetTopStories();
        Assert.NotNull(ids);
        Assert.Equal(500, ids.Count());
    }

    [Fact]
    public async Task TestGetStory()
    {
        var item = await _service.GetStory(45691127);

        Assert.NotNull(item);
        Assert.Equal(45691127, item.Id);
        Assert.InRange(item.DateTime, new DateTime(2025,1,1), new DateTime(2025,12,31));
    }

}
