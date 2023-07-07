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
            var curriculumList = new List<Curriculum>();

            using (var db = new InfoStudDB())
            {
                curriculumList = await db.Curriculums.ToListAsync();
            }
            return curriculumList;
        }

        [HttpPost]
        public async Task<List<Curriculum>> Post([FromBody]Curriculum curriculum)
        {
            using(var db = new InfoStudDB())
            {
                await db.Curriculums.AddAsync(curriculum);
                await db.SaveChangesAsync();
            }

            return await Get();
        }

        [HttpPut]
        public async Task<List<Curriculum>> Put(Curriculum curriculum)
        {
            using(var db = new InfoStudDB())
            {
                var editCurriculum = await db.Curriculums.FindAsync(curriculum.Id);

                if (editCurriculum != null)
                {
                    editCurriculum.Faculty = curriculum.Faculty;
                    editCurriculum.Speciality = curriculum.Speciality;
                    editCurriculum.Course = curriculum.Course;
                    editCurriculum.Group = curriculum.Group;

                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }

        [HttpDelete("{id}")]
        public async Task<List<Curriculum>> Delete(int id)
        {
            using(var db = new InfoStudDB())
            {
                var deleteSelectedCurriculum = await db.Curriculums.FindAsync(id);

                if(deleteSelectedCurriculum != null)
                {
                    db.Curriculums.Remove(deleteSelectedCurriculum);

                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }
    }
}
