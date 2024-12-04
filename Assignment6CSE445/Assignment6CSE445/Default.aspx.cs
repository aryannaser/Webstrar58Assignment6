using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;

namespace Assignment6CSE445
{
    public partial class _Default : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadTrendingNewsAsync();
            }
        }

        private async Task LoadTrendingNewsAsync()
        {
            try
            {
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = await client.GetAsync("trending-news");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var newsArticles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TrendingTopicsService.NewsArticle>>(jsonResponse);

                        // Bind the data to the Repeater
                        NewsRepeater.DataSource = newsArticles;
                        NewsRepeater.DataBind();
                    }
                    else
                    {
                        Response.Write("<p>Error fetching news data.</p>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<p>Error: {ex.Message}</p>");
            }
        }
    }
}