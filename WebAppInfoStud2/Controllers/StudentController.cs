using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Student>> Get()
        {
            var studList = new List<Student>();

            using (var db = new InfoStudDB())
            {
                studList = await db.Students.Include(s => s.Address).Include(s => s.Curriculum).Include(s => s.Contact).ToListAsync();
            }

            return studList;
        }

        [HttpPost]
        public async Task<List<Student>> Post([FromBody] Student student)
        {
            using(var db = new InfoStudDB())
            {
                await db.Students.AddAsync(student);
                await db.SaveChangesAsync();
            }
            return await Get();
        }

        [HttpPut]
        public async Task<List<Student>> Put([FromBody] Student student)
        {
            using (var db = new InfoStudDB())
            {
                var editStudent = await db.Students.FindAsync(student.Id);

                if (editStudent != null)
                {
                    db.Entry(editStudent).Reference(edS => edS.Address).Load();
                    db.Entry(editStudent).Reference(edS => edS.Curriculum).Load();
                    db.Entry(editStudent).Reference(edS => edS.Contact).Load();

                    editStudent.FullName = student.FullName;
                    editStudent.Address.City = student.Address.City;
                    editStudent.Address.PostIndex = student.Address.PostIndex;
                    editStudent.Address.Street = student.Address.Street;
                    editStudent.Curriculum.Faculty = student.Curriculum.Faculty;
                    editStudent.Curriculum.Speciality = student.Curriculum.Speciality;
                    editStudent.Curriculum.Course = student.Curriculum.Course;
                    editStudent.Curriculum.Group = student.Curriculum.Group;
                    editStudent.Contact!.Phone = student.Contact!.Phone;
                    editStudent.Contact.Email = student.Contact.Email;

                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }

        [HttpDelete("{id}")]
        public async Task<List<Student>> Delete(int id)
        {
            using(var db = new InfoStudDB())
            {
                var student = await db.Students.FindAsync(id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }
    }
}
