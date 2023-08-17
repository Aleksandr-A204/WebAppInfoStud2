using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllStudents(string? keywordSearch, string? sortProperty, string? sortType)
        {
            keywordSearch = keywordSearch?.ToLower() ?? string.Empty;
            var students = new List<Student>();

            using (var db = new StudentContext())
            {
                IQueryable<Student> allStudents = db.Students;

                if (keywordSearch != string.Empty)
                    allStudents = allStudents.Where(s => EF.Functions.Like(s.FullName.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.City.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Street.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Postindex.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Faculty.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Speciality.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Course.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Group.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Phone.ToLower(), $"%{keywordSearch}%")
                    || EF.Functions.Like(s.Email.ToLower(), $"%{keywordSearch}%"));


                if (sortType is not null && sortProperty is not null && sortType != "None")
                    allStudents = sortType == "Asc" ? allStudents.OrderBy(s => EF.Property<string>(s, sortProperty))
                        : allStudents.OrderByDescending(s => EF.Property<string>(s, sortProperty));

                students = await allStudents.ToListAsync();
            }

            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    await db.Students.AddAsync(student);

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось добавить студента.\n{ex}");
            }

            return Ok("Студент добавлен успешно.");
        }

        [HttpPut]
        public async Task<ActionResult> Put(Student student)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    db.Entry(student).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось редактировать студента.\n{ex}");
            }

            return Ok("Студент редактирован успешно.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var student = new Student { Id = id };

                    db.Entry(student).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось удалить студента.\n{ex}");
            }

            return Ok("Студент удален успешно.");
        }
    }
}
