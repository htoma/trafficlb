using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using trafficlb.Models;

namespace trafficlb.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IDistributedCache cache)
        {
            var cacheKey = "topselling";
            var albums = JsonConvert.DeserializeObject<List<Album>>(await cache.GetStringAsync(cacheKey) ?? "");
            if (albums == null || !albums.Any())
            {
                albums = await GetTopSellingAlbums();
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(albums),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
            }

            return View();
        }

        private Task<List<Album>> GetTopSellingAlbums()
        {
            // todo: talk to SQL repo
            return Task.FromResult(new List<Album>
            {
                new Album
                {
                    Band = "Milk and Bone",
                    Name = "Deception Bay"
                }
            });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
