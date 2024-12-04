using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Assignment6CSE445
{
    public class WeatherController : ApiController
    {
        private readonly string _apiKey = "84397d46e90f45db800185249241711"; // Your WeatherAPI key
        private readonly string _baseUrl = "https://api.weatherapi.com/v1";

        // GET: api/weather/forecast
        [HttpGet]
        [Route("api/weather/forecast")]
        public async Task<IHttpActionResult> GetForecast(string query, int days = 3) // Default to 2 days
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query parameter is required.");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={query}&days={days}";
                    var response = await client.GetStringAsync(url);

                    // Deserialize the response into a dynamic object
                    var jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                    return Ok(jsonResponse); // Return the JSON response directly
                }
            }
            catch (HttpRequestException ex)
            {
                return InternalServerError(new Exception("Error calling WeatherAPI", ex));
            }
        }
    }
}
