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
        [HttpGet]
        public async Task<List<Student>> Get()
        {
            var studentsList = new List<Student>();

            using (var db = new StudentContext())
            {
                studentsList = await db.Students.Include(s => s.Contact).ToListAsync();
            }

            return studentsList;
        }

        [HttpPost]
        public async Task<List<Student>> Post(Student student)
        {
            using (var db = new StudentContext())
            {
                await db.Students.AddAsync(student);

                await db.SaveChangesAsync();
            }

            return await Get();
        }

        [HttpPut]
        public async Task<List<Student>> Put(Student student)
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
            }

            return await Get();
        }

        [HttpDelete("{id}")]
        public async Task<List<Student>> Put(long id)
        {
            using (var db = new StudentContext())
            {
                var editStudent = await db.Students.FindAsync(id);

                if (editStudent != null)
                {
                    db.Students.Remove(editStudent);

                    await db.SaveChangesAsync();
                }
            }

            return await Get();
        }

    }
}
