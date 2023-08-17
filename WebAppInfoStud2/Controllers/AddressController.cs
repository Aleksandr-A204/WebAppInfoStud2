using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllAddresses(string? keywordSearch)
        {
            var addresses = new List<Address>();
            keywordSearch = keywordSearch?.ToLower() ?? string.Empty;

            using (var db = new StudentContext())
            {
                IQueryable<Address> alladdresses = db.Addresses.Include(a => a.City).Include(a => a.Postindex).Include(a => a.Street);

                if (keywordSearch != string.Empty)
                    alladdresses = alladdresses.Where(a => EF.Functions.Like(a.City.City.ToLower(), $"%{keywordSearch}%")
                        || EF.Functions.Like(a.Street.Street.ToLower(), $"%{keywordSearch}%")
                        || EF.Functions.Like(a.Postindex.PostIndex.ToLower(), $"%{keywordSearch}%"));

                addresses = await alladdresses.ToListAsync();
            }

            return Ok(addresses);
        }

        [HttpPost]
        public async Task<ActionResult> PostAddress([FromBody] Address address)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    await db.Addresses.AddAsync(address);

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось добавить адрес.\n{ex}");
            }

            return Ok("Адрес добавлен успешно.");
        }

        [HttpPut]
        public async Task<ActionResult> PutAddress(Address address)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    db.Entry(address).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось редактировать адрес.\n{ex}");
            }

            return Ok("Адрес редактирован успешно.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var address = new Address { Id = id };

                    db.Entry(address).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Ошибка! Не удалось удалить адрес.\n{ex}");
            }

            return Ok("Адрес удален успешно.");

        }
    }
}
