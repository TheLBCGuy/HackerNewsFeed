namespace NewsService;

public interface IItemDeserializer
{
    public T? Deserialize<T>(string json);
}
