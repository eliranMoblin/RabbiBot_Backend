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
        /// <summary>
        /// Get ALl Images from server
        /// </summary>
        /// <returns></returns>
        public async Task<List<Images>> GetImages()
        {
            string result;
            using (var webClient = new WebClient())
            {
                result = await webClient.DownloadStringTaskAsync("https://alwayscleandaysserver.azurewebsites.net/images/getimages");
            }
            if (string.IsNullOrWhiteSpace(result)) return null;
            var images = JsonConvert.DeserializeObject<List<Images>>(result);
            return images;
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Images> GetById(Guid id)
        {
            string result;
            using (var webClient = new WebClient())
            {
                result = await webClient.DownloadStringTaskAsync("https://alwayscleandaysserver.azurewebsites.net/images/GetById/{id}");
            }
            if (string.IsNullOrWhiteSpace(result)) return null;
            var images = JsonConvert.DeserializeObject<Images>(result);
            return images;
        }
    }
}
