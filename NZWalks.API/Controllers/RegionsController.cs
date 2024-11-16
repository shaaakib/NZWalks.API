using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // GET ALL REGIONS
        // https://localhost:1234/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
               new Region
               {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKl",
                    RegionImageUrl = "https://cdn.britannica.com/99/61399-050-B867F67F/skyline-Auckland-New-Zealand-Westhaven-Marina.jpg"
               },
                new Region
               {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://wellingtonnz.bynder.com/transform/84545bb1-49b2-46f6-ab75-cb96f98404b6/Mount-Victoria-073"
               }
            };

            return Ok(regions);
        }
    }
}
