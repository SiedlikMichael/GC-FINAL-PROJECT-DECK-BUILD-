using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CardsController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> FetchCardInfo()
        {
            try
            {
                string apiUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php?fname=Exodia";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var rootResponse = JsonConvert.DeserializeObject<RootResponse>(responseBody);

                if (rootResponse != null && rootResponse.Data != null && rootResponse.Data.Count > 0)
                {
                    return Ok(rootResponse.Data);
                }
                else
                {
                    return NotFound("No data found in the API response.");
                }
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
            catch (JsonException e)
            {
                return StatusCode(500, $"Deserialization error: {e.Message}");
            }
        }
    }
}
