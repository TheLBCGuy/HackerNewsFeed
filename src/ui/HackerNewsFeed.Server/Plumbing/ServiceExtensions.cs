using Caching;
using NewsService;
using ServiceContract;

namespace HackerNewsFeed.Server.Plumbing
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddStoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NewsOptions>(
                configuration.GetSection(NewsOptions.Position));

            services.AddSingleton(ItemDeserializer.Instance);
            services.AddHttpClient<IStoryService, StoryService>();

            if (configuration.GetValue<bool>("IntegrationTestRun"))
            {
                services.AddSingleton<IStoryService, StoryServiceMock>();
            }
            else
            {
                services.AddSingleton<IStoryService, StoryService>();
            }
            services.TryDecorate<IStoryService, StoryServiceCache>();
            services.AddSingleton<IIndexer>(LuceneIndexer.Instance);
            services.AddSingleton<IStoryIndexerService, StoryIndexerService>();

            if (configuration.GetValue<bool>("IndexStoriesOnStartup"))
            {
                services.AddHostedService<StoryHostedService>();
            }

            return services;
        }
    }
}
