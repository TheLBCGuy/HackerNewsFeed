using DataContract;

namespace NewsService;

public class NewsOptions : INewsOptions
{
    public const string Position = "News";

    public string BaseUrl { get; init; } = null!;
}
