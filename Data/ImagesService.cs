using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models;
using Images = Entity.Images;

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

        [HttpPost]
        public async Task<ActionResult> Update(Color color,int status)
        {
            Colors colors = new Colors
            {
                Hue = Convert.ToInt32(HSVColor.GetHSV(color).Hue),
                Saturation = Convert.ToInt32(HSVColor.GetHSV(color).Saturation),
                Value = Convert.ToInt32(HSVColor.GetHSV(color).Value),
                Status = status,
                CreateDate = DateTime.UtcNow

            };
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(colors.ToString(), Encoding.UTF8, "application/json");

                var result =await client.PostAsync("https://localhost:44387/images/Update", content);
            }

            
            return null;
        }
    }
}
