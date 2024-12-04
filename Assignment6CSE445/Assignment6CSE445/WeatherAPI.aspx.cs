using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace Assignment6CSE445
{
    public partial class WeatherAPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No action needed on initial page load
        }

        protected async void FetchWeatherButton_Click(object sender, EventArgs e)
        {
            string zipCode = ZipCodeInput.Text; // Get ZIP code input
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                WeatherLabel.Text = "Please enter a valid ZIP code.";
                return;
            }

            try
            {
                // Fetch the 5-day forecast from the local API
                var weatherData = await GetWeatherForecastAsync(zipCode);
                WeatherLabel.Text = weatherData; // Display the formatted forecast data
            }
            catch (Exception ex)
            {
                WeatherLabel.Text = $"Error fetching weather data: {ex.Message}";
            }
        }

        private async Task<string> GetWeatherForecastAsync(string zipCode)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call the local API endpoint for 5-day forecast
                string apiUrl = $"https://localhost:44302/api/weather/forecast?query={zipCode}&days=3";
                var response = await client.GetStringAsync(apiUrl);

                // Parse the JSON response
                JObject json = JObject.Parse(response);

                // Get the city name
                string cityName = (string)json["location"]["name"];

                // Get the forecast days
                JArray forecastDays = (JArray)json["forecast"]["forecastday"];

                // Format the forecast data
                string result = $"<h3>5-Day Weather Forecast for {cityName}</h3><ul>";
                foreach (var day in forecastDays)
                {
                    string date = (string)day["date"];
                    string temp = (string)day["day"]["avgtemp_f"]; // Temperature in Fahrenheit
                    string weatherType = (string)day["day"]["condition"]["text"]; // Weather type
                    result += $"<li>{date}: {temp}°F - {weatherType}</li>";
                }
                result += "</ul>";

                return result;
            }
        }
    }
}
