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
            var contactList = new List<Contact>();

            using(var db = new InfoStudDB())
            {
                contactList = await db.Contacts.ToListAsync();
            }

            return contactList;
        }

        [HttpPost]
        public async Task<List<Contact>> Post(Contact contact)
        {
            using(var db = new InfoStudDB())
            {
                await db.Contacts.AddAsync(contact);
                await db.SaveChangesAsync();
            }

            return await Get();
        }

        [HttpPut]
        public async Task<List<Contact>> Put(Contact contact)
        {
            using (var db = new InfoStudDB())
            {
                var editContact = await db.Contacts.FindAsync(contact.Id);

                if(editContact != null)
                {
                    editContact.Phone = contact.Phone;
                    editContact.Email = contact.Email;

                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }

        [HttpDelete("{id}")]
        public async Task<List<Contact>> Delete(int id)
        {
            using (var db = new InfoStudDB())
            {
                var deleteSelectedContact = await db.Contacts.FindAsync(id);

                if(deleteSelectedContact != null)
                {
                    db.Contacts.Remove(deleteSelectedContact);
                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }
    }
}
