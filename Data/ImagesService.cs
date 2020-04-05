using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Entity;
using Newtonsoft.Json;

namespace RabbiBot_Backend.Data
{
    public class ImagesService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }


        public async Task<List<Images>> GetImages()
        {
            WebClient client = new WebClient();
            var data = await client.DownloadStringTaskAsync("https://localhost:44387/images/getimages");
            var obj = JsonConvert.DeserializeObject<List<Images>>(data);
            return obj;

        }

        public async Task<Images> GetById(Guid id)
        {
            WebClient client = new WebClient();
            var data = await client.DownloadStringTaskAsync($"https://localhost:44387/images/GetById/{id}");
            var obj = JsonConvert.DeserializeObject<Images>(data);
            return obj;
        }
    }
}
