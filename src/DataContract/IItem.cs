using System.Text.Json.Serialization;

namespace DataContract;

public interface IItem
{
    public int Id { get; init; }
    public bool Deleted { get; init; }
    public string Type { get; init; }
    public string By { get; init; }
    public string? Text { get; init; }
    public bool Dead { get; init; }
    public string? Url { get; init; }
    public int Score { get; init; }
    public string Title { get; init; }
    [JsonPropertyName("time")]
    public int UnixTime { get; init; }
    public DateTime DateTime { get; }
}
