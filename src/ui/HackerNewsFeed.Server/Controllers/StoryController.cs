using DataContract;
using Microsoft.AspNetCore.Mvc;
using NewsService;
using ServiceContract;

namespace HackerNewsFeed.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController(
        IStoryService storyService,
        IStoryIndexerService storyIndexerService,
        ILogger<StoryController> logger
        ) : ControllerBase
    {
        private readonly IStoryIndexerService _storyIndexerService = storyIndexerService;
        private readonly ILogger<StoryController> _logger = logger;
        private readonly IStoryService _storyService = storyService;

        // TODO Refactor

        [HttpGet]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            if (pageIndex == 0 && pageSize == 0)
            {
                return await GetTopStories();
            }

            _logger.LogInformation($"Getting top stories for pageIndex {pageIndex} with pageSize {pageSize}...");
            var ids = await _storyService.GetTopStories();

            var min = pageIndex * pageSize;
            var max = min + pageSize;

            _logger.LogInformation("Retrieved {Count} top stories.", ids.Count());
            var items = new List<Item>();
            for (int index = min; index < max && index < ids.Count(); index++)
            {
                var id = ids.ElementAt(index);
                var item = await _storyService.GetStory(id);
                if (item == null) continue;
                if (isItemVetted(item))
                {
                    items.Add(item);
                }
            }
            var jsonData = new { stories = items, total = ids.Count() };
            return Ok(jsonData);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchPaginated([FromQuery] string searchTerm, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 25)
        {
            if (pageIndex == 0 && pageSize == 0)
            {
                return await GetTopStories();
            }

            _logger.LogInformation($"Searching top stories with '{searchTerm}' for pageIndex {pageIndex} with pageSize {pageSize}...");
            var ids = await _storyIndexerService.SearchStories(searchTerm);

            var min = pageIndex * pageSize;
            var max = min + pageSize;

            _logger.LogInformation("Retrieved {Count} top stories.", ids.Count());
            var items = new List<Item>();
            for (int index = min; index < max && index < ids.Count(); index++)
            {
                var id = ids.ElementAt(index);
                var item = await _storyService.GetStory(id);
                if (item == null) continue;
                if (isItemVetted(item))
                {
                    items.Add(item);
                }
            }
            var jsonData = new { stories = items, total = ids.Count() };
            return Ok(jsonData);
        }

        private async Task<IActionResult> GetTopStories()
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
            var jsonData = new { stories = items, total = items.Count() };
            return Ok(jsonData);
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
