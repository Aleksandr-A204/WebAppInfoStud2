using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Net;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Address>> Get()
        {
            var addList = new List<Address>(); 

            using (var db = new InfoStudDB())
            {
                addList = await db.Addresses.ToListAsync();
            }
            return addList;
        }

        [HttpPost]
        public async Task<List<Address>> Post([FromBody] Address address)
        {
            using (var db = new InfoStudDB())
            {
                await db.Addresses.AddAsync(address);
                await db.SaveChangesAsync();
            }

            return await Get();
        }

        [HttpPut]
        public async Task<List<Address>> Put(Address address)
        {
            using (var db = new InfoStudDB())
            {
                var editAddress = await db.Addresses.FindAsync(address.Id);

                if (editAddress != null)
                {
                    editAddress.City = address.City;
                    editAddress.PostIndex = address.PostIndex;
                    editAddress.Street = address.Street;

                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }

        [HttpDelete("{id}")]
        public async Task<List<Address>> Delete(int id)
        {
            using (var db = new InfoStudDB())
            {
                var address = await db.Addresses.FindAsync(id);

                if (address != null)
                {
                    db.Addresses.Remove(address);

                    await db.SaveChangesAsync();
                }
            }
            return await Get();
        }
    }
}
