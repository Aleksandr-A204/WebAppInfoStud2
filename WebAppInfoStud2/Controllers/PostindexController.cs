using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostindexController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var postindexList = new List<PostindexTable>();

            using (var db = new StudentContext())
            {
                postindexList = await db.PostindexTables.ToListAsync();
            }

            return Ok(postindexList);
        }
    }
}
