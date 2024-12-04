using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Assignment6CSE445.Controllers
{
    public class StocksController : ApiController
    {
        [HttpGet]
        [Route("api/stocks/{symbol}")]
        public async Task<IHttpActionResult> GetStockQuote(string symbol)
        {
            // Alpha Vantage API key
            string apiKey = "1S2EHKF18LZ3BMEP";// Replace with your actual API key if different
            string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        // Log the raw JSON response for debugging
                        System.Diagnostics.Debug.WriteLine($"Raw JSON Response: {result}");

                        // Parse the JSON response
                        var json = Newtonsoft.Json.Linq.JObject.Parse(result);

                        // Check if "Global Quote" exists
                        var globalQuote = json["Global Quote"];
                        if (globalQuote == null || !globalQuote.HasValues)
                        {
                            return NotFound(); // Return a 404 if no stock data is found
                        }

                        // Extract relevant stock data
                        var stockData = new
                        {
                            Symbol = globalQuote["01. symbol"]?.ToString() ?? "N/A",
                            Price = globalQuote["05. price"]?.ToString() ?? "N/A",
                            Date = globalQuote["07. latest trading day"]?.ToString() ?? "N/A",
                            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // Add current timestamp
                        };

                        return Ok(stockData);
                    }
                    else
                    {
                        string errorMessage = $"Failed to fetch stock data. HTTP Status: {response.StatusCode}";
                        System.Diagnostics.Debug.WriteLine(errorMessage);
                        return BadRequest(errorMessage);
                    }
                }
                catch (Newtonsoft.Json.JsonReaderException jsonEx)
                {
                    System.Diagnostics.Debug.WriteLine($"JSON Parsing Error: {jsonEx.Message}");
                    return InternalServerError(new Exception("Error parsing stock data response. Please try again later."));
                }
                catch (Exception ex)
                {
                    // Catch all other exceptions
                    System.Diagnostics.Debug.WriteLine($"Unexpected Error: {ex.Message}");
                    return InternalServerError(ex);
                }
            }
        }
    }
}
