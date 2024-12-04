using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Assignment6CSE445
{
    public class TrendingServiceClient
    {
        private readonly HttpClient _httpClient;

        public TrendingServiceClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:<port>/api/trending/")
            };
        }

        public async Task<List<string>> GetTrendingNewsAsync()
        {
            var response = await _httpClient.GetAsync("trending-news");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(json);
        }

        public async Task<List<string>> GetTrendingTopicsAsync()
        {
            var response = await _httpClient.GetAsync("serp-topics");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
    }
}