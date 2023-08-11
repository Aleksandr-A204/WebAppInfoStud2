using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        [HttpGet]
        public async Task<List<StreetTable>> Get()
        {
            var streetList = new List<StreetTable>();

            using (var db = new StudentContext())
            {
                streetList = await db.StreetTables.ToListAsync();
            }
            return streetList;
        }
    }
}
