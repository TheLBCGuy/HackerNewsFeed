using Xunit;

namespace NewsService.Test.Item;

public class DeserializeTest
{
    [Fact]
    public void TestItemDeserialization()
    {
        var deserializer = ItemDeserializer.Instance;

        var item = deserializer.Deserialize<DataContract.Item>(FileLoader.LoadItemResponseJson("item_45691127"));
        Assert.NotNull(item);
        Assert.Equal(45691127, item.Id);
        Assert.Equal("fujigawa", item.By);
        Assert.Equal(147, item.Score);
        Assert.Equal(1761283924, item.UnixTime);
        Assert.Equal(new DateTime(2025, 10, 24, 5, 32, 4), item.DateTime);
        Assert.Equal("Alaska Airlines' statement on IT outage", item.Title);
        Assert.Equal("story", item.Type);
        Assert.Equal("https://news.alaskaair.com/on-the-record/alaska-statement-on-it-outage/", item.Url);
        Assert.False(item.Deleted);
        Assert.False(item.Dead);
        Assert.Null(item.Text);
    }
}
