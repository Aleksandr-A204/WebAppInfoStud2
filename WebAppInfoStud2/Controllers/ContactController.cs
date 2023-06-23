using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Contact>> Get()
        {
            var contList = new List<Contact>();

            using(var db = new InfoStudDB())
            {
                contList = await db.Contacts.ToListAsync();
            }

            return contList;
        }

        [HttpPost]
        public async Task<string> Post(Contact contact)
        {
            using(var db = new InfoStudDB())
            {
                await db.Contacts.AddAsync(contact);
                await db.SaveChangesAsync();
            }

            return "Added successfully";
        }

        [HttpPut]
        public async Task<string> Put(Contact contact)
        {
            using (var db = new InfoStudDB())
            {
                var editCont = await db.Contacts.FindAsync(contact.Id);

                if(editCont != null)
                {
                    editCont.Phone = contact.Phone;
                    editCont.Email = contact.Email;

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
            using (var db = new InfoStudDB())
            {
                var editCont = await db.Contacts.FindAsync(id);

                if(editCont != null)
                {
                    db.Contacts.Remove(editCont);
                    await db.SaveChangesAsync();
                }
                else
                {
                    return "Error! Failed to delete the data.";
                }
            }
            return "Deleted successfully";
        }
    }
}
