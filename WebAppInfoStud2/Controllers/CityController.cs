using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cities = new List<CityTable>();

            using (var db = new StudentContext())
            {
                IQueryable<CityTable> allCities = db.CityTables;

                cities = await allCities.ToListAsync();
            }
            return Ok(cities);
        }
    }
}
