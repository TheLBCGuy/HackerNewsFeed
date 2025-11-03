using DataContract;

namespace NewsService;

public class NewsOptions : INewsOptions
{
    public string BaseUrl { get; init; } = null!;
}
