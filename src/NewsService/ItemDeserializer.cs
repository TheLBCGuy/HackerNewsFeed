using System.Text.Json;

namespace NewsService;

public class ItemDeserializer
{
    public ItemDeserializer() { }
    public static readonly ItemDeserializer Instance = new();

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
