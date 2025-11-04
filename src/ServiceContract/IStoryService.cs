using DataContract;

namespace ServiceContract
{
    public interface IStoryService
    {
        public Task<IEnumerable<int>> GetTopStories();

        public Task<IEnumerable<Item>> GetStories(IEnumerable<int> ids);

        public Task<Item?> GetStory(int id);
    }
}
