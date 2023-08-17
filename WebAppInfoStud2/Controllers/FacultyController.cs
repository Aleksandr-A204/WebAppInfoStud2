using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var facultyList = new List<FacultyTable>();

            using (var db = new StudentContext())
            {
                facultyList = await db.FacultyTables.ToListAsync();
            }

            return Ok(facultyList);
        }
    }
}
