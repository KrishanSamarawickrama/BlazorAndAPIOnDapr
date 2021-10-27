using System.Text.Json;

namespace BlazorAndAPIOnDapper.Web.SSR.Data
{
    public class WeatherForecastService
    {
        private readonly IHttpClientFactory clientFactory;               

        public WeatherForecastService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }                      

        public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(DateTime startDate)
        {   
            var client = clientFactory.CreateClient("dapr");
            var resp = await client.GetAsync("/v1.0/invoke/webapi/method/WeatherForecast");

            //var client = new HttpClient();
            //var resp = await client.GetAsync("http://webapi/WeatherForecast");

            if (!resp.IsSuccessStatusCode)
            {
                return Enumerable.Empty<WeatherForecast>();
            }
            var contentStream = await resp.Content.ReadAsStreamAsync();
            var products = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(contentStream,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return products;
        }
    }
}