using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Assignment6CSE445
{
    public class TrendingTopicsService
    {
        private readonly HttpClient _httpClient;
        private readonly string newsApiKey;
        private readonly string serpApiKey;

        public TrendingTopicsService()
        {
            _httpClient = new HttpClient();
            // Read API keys from Web.config
            newsApiKey = "aa61e24e281e46d08e2efec86d16a5f3";
            serpApiKey = "05c579199c40e882763c1cee873d0e0988b51d011195c48e030dea721dc25666";
        }

        // Method to fetch trending news from NewsAPI
        public async Task<List<NewsArticle>> GetTrendingNews()
        {
            var requestUrl = $"https://newsapi.org/v2/top-headlines?country=us&apiKey={newsApiKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("User-Agent", "TrendingTopicsMonitor/1.0");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new List<NewsArticle> { new NewsArticle { Title = "Error fetching news." } };
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var newsData = JsonConvert.DeserializeObject<NewsApiResponse>(jsonResponse);

            return newsData?.Articles ?? new List<NewsArticle>();
        }

        // Method to fetch trending topics from SerpAPI (Google Trends) for the USA
        public async Task<List<TrendingTopic>> GetTrendingTopicsFromSerpAPI()
        {
            var requestUrl = $"https://serpapi.com/search.json?engine=google_trends&q=trending&data_type=RELATED_TOPICS&api_key={serpApiKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("User-Agent", "TrendingTopicsMonitor/1.0");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new List<TrendingTopic> { new TrendingTopic { Title = "Error fetching trending topics.", Type = "Error", Value = "", Link = "" } };
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var serpData = JsonConvert.DeserializeObject<SerpApiResponse>(jsonResponse);

            var trendingTopics = new List<TrendingTopic>();

            if (serpData?.RelatedTopics?.Rising != null)
            {
                foreach (var topic in serpData.RelatedTopics.Rising)
                {
                    trendingTopics.Add(new TrendingTopic
                    {
                        Title = topic.Topic.Title,
                        Type = topic.Topic.Type,
                        Value = topic.Value,
                        Link = topic.Link
                    });
                }
            }

            if (serpData?.RelatedTopics?.Top != null)
            {
                foreach (var topic in serpData.RelatedTopics.Top)
                {
                    trendingTopics.Add(new TrendingTopic
                    {
                        Title = topic.Topic.Title,
                        Type = topic.Topic.Type,
                        Value = topic.Value,
                        Link = topic.Link
                    });
                }
            }

            return trendingTopics;
        }

        // Model to represent the NewsAPI response
        private class NewsApiResponse
        {
            public List<NewsArticle> Articles { get; set; }
        }

        // Model to represent individual articles
        public class NewsArticle
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public DateTime? PublishedAt { get; set; }
        }

        // Model to represent the SerpAPI response
        private class SerpApiResponse
        {
            [JsonProperty("related_topics")]
            public RelatedTopics RelatedTopics { get; set; }
        }

        private class RelatedTopics
        {
            [JsonProperty("rising")]
            public List<TopicInfo> Rising { get; set; }

            [JsonProperty("top")]
            public List<TopicInfo> Top { get; set; }
        }

        private class TopicInfo
        {
            [JsonProperty("topic")]
            public TopicDetails Topic { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("link")]
            public string Link { get; set; }
        }

        private class TopicDetails
        {
            [JsonProperty("value")]
            public string Value { get; set; }  // This is the internal value or ID of the topic

            [JsonProperty("title")]
            public string Title { get; set; }  // The title of the topic

            [JsonProperty("type")]
            public string Type { get; set; }   // The type of the topic (e.g., 'Food', 'Topic', etc.)
        }

        public class TrendingTopic
        {
            public string Title { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
            public string Link { get; set; }
        }
    }
}