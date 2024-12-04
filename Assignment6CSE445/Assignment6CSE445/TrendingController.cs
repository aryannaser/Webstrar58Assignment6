using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Assignment6CSE445
{
    [RoutePrefix("api/Trending")]
    public class TrendingController : ApiController
    {
        private readonly TrendingTopicsService _trendingTopicsService;

        public TrendingController()
        {
            // Initialize the service (No arguments needed for TrendingTopicsService in .NET Framework)
            _trendingTopicsService = new TrendingTopicsService();
        }

        // Endpoint to get trending news from NewsAPI
        [HttpGet]
        [Route("trending-news")]
        public async Task<IHttpActionResult> GetTrendingNews()
        {
            var trendingNews = await _trendingTopicsService.GetTrendingNews();

            if (trendingNews == null || trendingNews.Count == 0)
            {
                return NotFound(); // Returns 404 if no news is found
            }

            return Ok(trendingNews); // Returns 200 OK with the news data
        }

        // Endpoint to get trending topics from SerpAPI
        [HttpGet]
        [Route("serp-topics")]
        public async Task<IHttpActionResult> GetTrendingTopicsSerp()
        {
            var trendingTopics = await _trendingTopicsService.GetTrendingTopicsFromSerpAPI();

            if (trendingTopics == null || trendingTopics.Count == 0)
            {
                return NotFound(); // Returns 404 if no topics are found
            }

            return Ok(trendingTopics); // Returns 200 OK with the topics data
        }
    }
}