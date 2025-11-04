using DataContract;
using Microsoft.AspNetCore.Mvc;
using ServiceContract;

namespace HackerNewsFeed.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController(
        IStoryService storyService,
        ILogger<StoryController> logger
        ) : ControllerBase
    {
        private readonly ILogger<StoryController> _logger = logger;
        private readonly IStoryService _storyService = storyService;

        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            var items = await GetTopStories();
            return items;
        }

        private async Task<IEnumerable<Item>> GetTopStories()
        {
            // TODO: Introduce parallel tasks here to speed up retrieval of stories

            _logger.LogInformation("Getting top stories ids...");
            var ids = await _storyService.GetTopStories();
            _logger.LogInformation("Retrieved {Count} top stories.", ids.Count());
            var items = new List<Item>();
            foreach (var id in ids)
            {
                var item = await _storyService.GetStory(id);
                if (item == null) continue;
                if (isItemVetted(item)) {
                    items.Add(item);
                }
            }
            return items;
        }

        private bool isItemVetted(Item item)
        {
            // TODO : What does 'dead' mean? Decide later if we want/need to filter out dead items

            if (item.Type != "story") return false;
            if (item.Url == null) return false;
            if (item.Deleted) return false;
            // if (item.Dead) return false;
            return true;
        }
    }
}
