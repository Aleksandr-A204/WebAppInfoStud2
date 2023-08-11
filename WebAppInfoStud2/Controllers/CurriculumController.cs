using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Curriculum>> GetCurriculums(string? keywordSearch)
        {
            var filterByCurriculum = new List<Curriculum>();

            using (var db = new StudentContext())
            {
                var curriculumList = await db.Curriculums.Include(c => c.Faculty)
                                                    .Include(c => c.Speciality)
                                                    .Include(c => c.Course)
                                                    .Include(c => c.Group).ToListAsync();

                if (keywordSearch is null || keywordSearch == string.Empty)
                    filterByCurriculum = curriculumList;
                else
                    filterByCurriculum.AddRange(curriculumList.Where(c => c.Faculty.Faculty.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)
                    || c.Speciality.Speciality.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)
                    || c.Course.Course.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)
                    || c.Group.Group.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)));
            }

            return filterByCurriculum;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Curriculum curriculum)
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
                return $"Ошибка! Не удалось добавить учебный план.\n{ex}";
            }

            return "Учебный план добавлен успешно.";
        }

        [HttpPut]
        public async Task<string> Put(Curriculum curriculum)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var editCurriculum = await db.Curriculums.FindAsync(curriculum.Id);

                    if (editCurriculum != null)
                    {
                        await db.Entry(editCurriculum).Reference(c => c.Faculty).LoadAsync();
                        await db.Entry(editCurriculum).Reference(c => c.Speciality).LoadAsync();
                        await db.Entry(editCurriculum).Reference(c => c.Course).LoadAsync();
                        await db.Entry(editCurriculum).Reference(c => c.Group).LoadAsync();

                        editCurriculum.FacultyId = curriculum.FacultyId;
                        editCurriculum.SpecialityId = curriculum.SpecialityId;
                        editCurriculum.CourseId = curriculum.CourseId;
                        editCurriculum.GroupId = curriculum.GroupId;

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка! Не удалось редактировать учебный план.\n{ex}";
            }

            return "Учебный план редактирован успешно.";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var curriculum = await db.Curriculums.FindAsync(id);

                    if (curriculum != null)
                    {
                        db.Curriculums.Remove(curriculum);

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка! Не удалось удалить учебный план.\n{ex}";
            }

            return "Учебный план удален успешно.";
        }
    }
}
