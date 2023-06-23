using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Curriculum>> Get()
        {
            var currList = new List<Curriculum>();

            using (var db = new InfoStudDB())
            {
                currList = await db.Curriculums.ToListAsync();
            }
            return currList;
        }

        [HttpPost]
        public async Task<string> Post([FromBody]Curriculum curriculum)
        {
            using(var db = new InfoStudDB())
            {
                await db.Curriculums.AddAsync(curriculum);
                await db.SaveChangesAsync();
            }

            return "Added successfully";
        }

        [HttpPut]
        public async Task<string> Put(Curriculum curriculum)
        {
            using(var db = new InfoStudDB())
            {
                var editCurr = await db.Curriculums.FindAsync(curriculum.Id);

                if (editCurr != null)
                {
                    editCurr.Faculty = curriculum.Faculty;
                    editCurr.Speciality = curriculum.Speciality;
                    editCurr.Cource = curriculum.Cource;
                    editCurr.Group = curriculum.Group;

                    await db.SaveChangesAsync();
                }
                else
                {
                    return "Error! Failed to edit the date.";
                }
            }
            return "Edited successfully";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            using(var db = new InfoStudDB())
            {
                var editCurr = await db.Curriculums.FindAsync(id);

                if(editCurr != null)
                {
                    db.Curriculums.Remove(editCurr);

                    await db.SaveChangesAsync();
                }
                else
                {
                    return "Error! Failed to delete the date.";
                }
            }
            return "Deleted successfully";
        }
    }
}
