namespace DataContract
{
    public interface IStory
    {
        public int Id { get; init; }
        public string Author { get; init; }
        public DateTime DateTime { get; init; }
        public string Title { get; init; }
        public string Text { get; init; }
        public string Url { get; init; }
        public int Score { get; init; }
    }
}
