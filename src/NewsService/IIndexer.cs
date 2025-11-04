using DataContract;

namespace NewsService
{
    public interface IIndexer
    {
        void Add(Item story);
        IEnumerable<int> Search(string query);
    }
}
