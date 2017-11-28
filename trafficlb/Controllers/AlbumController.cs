using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace trafficlb.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        [HttpGet]
        [Route("BestSellers")]
        public IEnumerable<Album> GetBestSellers()
        {
            return new[]
            {
                new Album
                {
                    Band = "Sales",
                    Name = "Sales LP"
                }
            };
        }
    }
}