using DataContract;
using ServiceContract;

namespace NewsService;

internal static class Extensions
{
    public static async Task<IEnumerable<Item>> GetStories(this IStoryService storyService, IEnumerable<int> ids)
    {
        var items = new List<Item>();
        foreach (var id in ids)
        {
            var item = await storyService.GetStory(id);
            if (item == null) continue;
            items.Add(item);
        }
        return items;
    }
}
