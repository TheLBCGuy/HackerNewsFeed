using System.Text.Json.Serialization;

namespace DataContract;

public class Item : IItem
{
    public int Id { get; init; }
    public bool Deleted { get; init; } = false;
    public string Type { get; init; } = null!;
    public string By { get; init; } = null!;
    public string? Text { get; init; }
    public bool Dead { get; init; } = false;
    public string? Url { get; init; }
    public int Score { get; init; }
    public string Title { get; init; } = null!;
    [JsonPropertyName("time")]
    public int UnixTime { get; init; }
    public DateTime DateTime
    {
        get { return DateTimeOffset.FromUnixTimeSeconds(UnixTime).DateTime; }
    }
}
