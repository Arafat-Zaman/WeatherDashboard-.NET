using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherDashboard.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiKey = "YOUR_API_KEY";  // Replace with your OpenWeather API key

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Weather/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Weather/GetWeather?city=cityName
        [HttpGet]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Json(new { error = "City name cannot be empty" });
            }

            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { error = "Could not fetch weather data." });
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject(responseBody);

            return Json(weatherData);
        }
    }
}
