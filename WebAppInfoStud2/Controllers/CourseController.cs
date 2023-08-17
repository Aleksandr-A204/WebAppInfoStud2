using Microsoft.AspNetCore.Mvc;

using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var courseList = new List<CourseEntity>() { 
                new CourseEntity { Id = 1, Course = "1" },
                new CourseEntity { Id = 2, Course = "2" },
                new CourseEntity { Id = 3, Course = "3" },
                new CourseEntity { Id = 4, Course = "4" },
                new CourseEntity { Id = 5, Course = "5" },
                new CourseEntity { Id = 6, Course = "6" },
            };

            return Ok(courseList);
        }
    }
}
