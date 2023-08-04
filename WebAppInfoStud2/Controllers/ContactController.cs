using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Contact>> Get()
        {
            var contactsList = new List<Contact>();

            using (var db = new StudentContext())
            {
                contactsList = await db.Contacts.ToListAsync();
            }

            return contactsList;
        }

        [HttpPost]
        public async Task<string> Post(Contact contact)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    await db.Contacts.AddAsync(contact);

                    await db.SaveChangesAsync();
                }
            }
            catch 
            {
                return "Ошибка! Не удалось добавить контакт.";
            }

            return "Контакт добавлен успешно.";
        }

        [HttpPut]
        public async Task<string> PutContact(Contact contact)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var editContact = await db.Contacts.FindAsync(contact.Id);

                    if (editContact != null)
                    {
                        editContact.Phone = contact.Phone;
                        editContact.Email = contact.Email;

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch 
            {
                return "Ошибка! Не удалось редактировать контакт.";
            }

            return "Контакт редактирован успешно.";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var contact = await db.Contacts.FindAsync(id);

                    if (contact != null)
                    {
                        db.Contacts.Remove(contact);

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch
            {
                return "Ошибка! Не удалось удалить контакт.";
            }

            return "Контакт редактирован успешно.";
        }
    }
}
