using NewsService;

namespace HackerNewsFeed.Server
{
    public class StoryHostedService(
        IStoryIndexerService indexerService,
        ILogger<StoryHostedService> logger
        ) : BackgroundService
    {
        private readonly IStoryIndexerService _indexerService = indexerService;
        private readonly ILogger<StoryHostedService> _logger = logger;
        private int _executionCount;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Story Hosted Service running.");

            await DoWork();

            //using PeriodicTimer timer = new(TimeSpan.FromSeconds(30));
            using PeriodicTimer timer = new(TimeSpan.FromMinutes(60));          // update stories and indexing every hour in the bg
            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWork();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Story Hosted Service is stopping.");
            }
        }

        private async Task DoWork()
        {
            int count = Interlocked.Increment(ref _executionCount);

            // Simulate work
            await _indexerService.IndexStories();

            _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
        }
    }
}
