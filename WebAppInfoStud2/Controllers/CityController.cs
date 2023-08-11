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
        public async Task<List<CityTable>> Get()
        {
            var cityList = new List<CityTable>();

            using (var db = new StudentContext())
            {
                cityList = await db.CityTables.ToListAsync();
            }
            return cityList;
        }
    }
}
