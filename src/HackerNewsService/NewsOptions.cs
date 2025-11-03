using DataContract;

namespace NewsService;

public class NewsOptions : INewsUrlOptions
{
    public string BaseUrl { get; init; } = null!;
}
