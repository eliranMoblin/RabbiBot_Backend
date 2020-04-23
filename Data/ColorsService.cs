using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models;
using Images = Entity.Images;

namespace RabbiBot_Backend.Data
{
    public class ColorsService
    {
        public async Task<List<Colors>> Get()
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync($"https://localhost:44387/Colors/Get");
            var obj = JsonConvert.DeserializeObject<List<Colors>>(data);
            return obj;
        }


        [HttpPost]
        public async Task<ActionResult> Update(ColorViewModel color, int status)
        {
            var colors = new Colors
            {
                Hue = color.Hue,
                Saturation = color.Saturation,
                Value = color.Value,
                Status = status,
                R = color.R,
                B = color.B,
                G=color.G,
                CreateDate = DateTime.UtcNow
            };
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(colors), Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://localhost:44387/Colors/Update", content);
            }
            return null;
        }

        
        public async Task<ImageResult> GetImageColor(Guid id)
        {

            WebClient client = new WebClient();
            var data = await client.DownloadStringTaskAsync($"https://localhost:44387/images/GetById/{id}");
            var obj = JsonConvert.DeserializeObject<Images>(data);
            List<Colors> colorsList = await GetColors();
            var request = WebRequest.Create(obj.Url);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var bmp = new Bitmap(responseStream);
            var colorsViewModelList = new List<ColorViewModel>();
            
            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    var color = bmp.GetPixel(x, y);
                    ColorViewModel model = new ColorViewModel
                    {
                        Hue = Convert.ToInt32(HSVColor.GetHSV(color).Hue),
                        Saturation = Convert.ToInt32(HSVColor.GetHSV(color).Saturation),
                        Value = Convert.ToInt32(HSVColor.GetHSV(color).Value),
                        R = color.R,
                        B= color.B,
                        G= color.G
                        };

                    var reuslt = colorsViewModelList.SingleOrDefault(x => x.Hue == model.Hue && x.Saturation == model.Saturation && x.Value == model.Value);
                    if (reuslt == null)
                    {

                        var ccc = colorsList;
                        var xx = colorsList.SingleOrDefault(x => x.Hue == model.Hue && x.Saturation == model.Saturation && x.Value == model.Value);
                        if (xx == null)
                        {
                            colorsViewModelList.Add(model);
                        }


                    }

                }
                
            }

            ImageResult imageResult= new ImageResult
            {
                ColorViewModels = colorsViewModelList,
                Images =  obj
            };
            return imageResult;
        }

        private async Task<List<Colors>> GetColors()
        {
            WebClient client = new WebClient();
            var data = await client.DownloadStringTaskAsync($"https://localhost:44387/Colors/Get");
            var obj = JsonConvert.DeserializeObject<List<Colors>>(data);
            return obj;
        }

        public async Task<List<Colors>> GetColorByStatus(int statusId)
        {
            WebClient client = new WebClient();
            var data = await client.DownloadStringTaskAsync($"https://localhost:44387/Colors/Get");
            var colorsList = JsonConvert.DeserializeObject<List<Colors>>(data);
            var xxx = colorsList.Where(x => x.Status == statusId).Take(15).ToList();
            
            return xxx;
        }
    }
}
