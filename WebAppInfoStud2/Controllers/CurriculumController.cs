using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllCurriculums(string? keywordSearch)
        {
            var curriculums = new List<Curriculum>();
            keywordSearch = keywordSearch?.ToLower() ?? string.Empty;

            using (var db = new StudentContext())
            {
                IQueryable<Curriculum> allCurriculums = db.Curriculums.Include(c => c.Faculty).Include(c => c.Speciality);

                if (keywordSearch != string.Empty)
                        allCurriculums = allCurriculums.Where(a => EF.Functions.Like(a.Faculty.Faculty.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(a.Speciality.Speciality.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(a.Course.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(a.Group.ToLower(), $"%{keywordSearch}%"));

                curriculums = await allCurriculums.ToListAsync();
            }

            return Ok(curriculums);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Curriculum curriculum)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    await db.Curriculums.AddAsync(curriculum);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось добавить учебный план.\n{ex}");
            }

            return Ok("Учебный план добавлен успешно.");
        }

        [HttpPut]
        public async Task<ActionResult> Put(Curriculum curriculum)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    db.Entry(curriculum).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось редактировать учебный план.\n{ex}");
            }

            return Ok("Учебный план редактирован успешно.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var curriculum = new Curriculum { Id = id };
                    db.Entry(curriculum).State = EntityState.Deleted;

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось удалить учебный план.\n{ex}");
            }

            return Ok("Учебный план удален успешно.");
        }
    }
}
