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
            var courseList = new List<string>() { "1", "2", "3", "4", "5", "6" };

            return Ok(courseList);
        }
    }
}
