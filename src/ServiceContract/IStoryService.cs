using DataContract;

namespace ServiceContract
{
    public interface IStoryService
    {
        public Task<IEnumerable<int>> GetTopStories();

        public Task<Item?> GetStory(int id);
    }
}
