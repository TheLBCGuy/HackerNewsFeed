using System;
namespace HackerNewsService.Models;

public class Item
{
    public int Id { get; init; }
    public bool Deleted { get; init; }
    public string Type { get; init; } = null!;
    public string By { get; init; } = null!;
    public DateTimeOffset Time { get; init; }
    public string Text { get; init; } = null!;
    public bool Dead { get; init; }
    public string? Url { get; init; } 
    public int Score { get; init; }
    public string Title { get; init; } = null!;
    
}
