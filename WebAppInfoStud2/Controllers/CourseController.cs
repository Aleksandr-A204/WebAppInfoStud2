using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        public async Task<List<CourseTable>> Get()
        {
            var courseList = new List<CourseTable>();

            using (var db = new StudentContext())
            {
                courseList = await db.CourseTables.ToListAsync();
            }
            return courseList;
        }
    }
}
