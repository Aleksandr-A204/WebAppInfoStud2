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
        public async Task<ActionResult> Get()
        {
            var allStreet= new List<StreetTable>();

            using (var db = new StudentContext())
            {
                allStreet = await db.StreetTables.ToListAsync();
            }
            return Ok(allStreet);
        }
    }
}
