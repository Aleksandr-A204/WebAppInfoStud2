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
        public async Task<List<Address>> GetAllAddresses(string? keywordSearch)
        {
            var filterByAddress = new List<Address>();

            using (var db = new StudentContext())
            {
                var addressList = await db.Addresses.Include(a => a.City).Include(a => a.Postindex).Include(a => a.Street).ToListAsync();

                if (keywordSearch is null)
                    filterByAddress = addressList;
                else
                    filterByAddress.AddRange(addressList.Where(a => a.City.City.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)
                    || a.Street.Street.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)
                    || a.Postindex.PostIndex.Contains(keywordSearch, StringComparison.OrdinalIgnoreCase)));
            }

            return filterByAddress;
        }

        [HttpPost]
        public async Task<string> PostAddress([FromBody] Address address)
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
                return $"Ошибка! Не удалось добавить адрес.\n{ex}";
            }

            return "Адрес добавлен успешно.";
        }

        [HttpPut]
        public async Task<string> PutAddress(Address address)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var editAddress = await db.Addresses.FindAsync(address.Id);

                    if (editAddress != null)
                    {
                        db.Entry(editAddress).Reference(a => a.City).Load();
                        db.Entry(editAddress).Reference(a => a.Postindex).Load();
                        db.Entry(editAddress).Reference(a => a.Street).Load();

                        editAddress.CityId = address.CityId;
                        editAddress.PostindexId = address.PostindexId;
                        editAddress.StreetId = address.StreetId;

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка! Не удалось редактировать адрес.\n{ex}";
            }

            return "Адрес редактирован успешно.";
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteAddress(long id)
        {
            try
            {
                using (var db = new StudentContext())
                {
                    var address = await db.Addresses.FindAsync(id);

                    if (address != null)
                    {
                        db.Addresses.Remove(address);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка! Не удалось удалить адрес.\n{ex}";
            }

            return "Адрес удален успешно.";

        }
    }
}
