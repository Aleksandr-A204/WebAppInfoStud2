using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        [HttpGet]
        public async Task<List<GroupTable>> Get()
        {
            var groupList = new List<GroupTable>();

            using (var db = new StudentContext())
            {
                groupList = await db.GroupTables.ToListAsync();
            }
            return groupList;
        }
    }
}
