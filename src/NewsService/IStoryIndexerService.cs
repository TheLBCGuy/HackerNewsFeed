namespace NewsService;

public interface IStoryIndexerService
{
    public Task IndexStories();
    public Task<IEnumerable<int>> SearchStories(string term);
}

