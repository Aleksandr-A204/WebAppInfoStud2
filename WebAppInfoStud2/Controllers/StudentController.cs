using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("{keywordSearch}")]
        public async Task<List<Student>> Get(string keywordSearch)
        {
            var studentsList = new List<Student>();

            using (var db = new StudentContext())
            {
                studentsList = await db.Students.Include(s => s.Contact).ToListAsync();

                Console.WriteLine();
                //studentsList.Count
            }

            return studentsList;
        }

        [HttpPost]
        public async Task<string> Post(Student student)
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
                        await db.Entry(editStudent).Reference(s => s.Contact).LoadAsync();

                        editStudent.FullName = student.FullName;
                        editStudent.City = student.City;
                        editStudent.Postindex = student.Postindex;
                        editStudent.Street = student.Street;
                        editStudent.Faculty = student.Faculty;
                        editStudent.Speciality = student.Speciality;
                        editStudent.Course = student.Course;
                        editStudent.Group = student.Group;
                        editStudent.Contact.Phone = student.Contact.Phone;
                        editStudent.Contact.Email = student.Contact.Email;

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
        public async Task<string> Put(long id)
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
