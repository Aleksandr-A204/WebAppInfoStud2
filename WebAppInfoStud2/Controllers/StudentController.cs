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
        public async Task<List<Student>> GetAllStudents(string? keywordSearch, string? sortProperty, string? sortType)
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


            return students;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Student student)
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
                return $"Ошибка! Не удалось добавить студента.\n{ex}";
            }

            return "Студент добавлен успешно.";
        }

        [HttpPut]
        public async Task<string> Put(Student student)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var editStudent = await db.Students.FindAsync(student.Id);

                    if (editStudent != null)
                    {
                        editStudent.FullName = student.FullName;
                        editStudent.City = student.City;
                        editStudent.Postindex = student.Postindex;
                        editStudent.Street = student.Street;
                        editStudent.Faculty = student.Faculty;
                        editStudent.Speciality = student.Speciality;
                        editStudent.Course = student.Course;
                        editStudent.Group = student.Group;
                        editStudent.Phone = student.Phone;
                        editStudent.Email = student.Email;

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка! Не удалось редактировать студента.\n{ex}";
            }

            return "Студент редактирован успешно.";
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteStudent(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var editStudent = await db.Students.FindAsync(id);

                    if (editStudent != null)
                    {
                        db.Students.Remove(editStudent);

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка! Не удалось удалить студента.\n{ex}";
            }

            return "Студент удален успешно.";
        }
    }
}
