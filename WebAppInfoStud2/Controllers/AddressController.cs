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
        public async Task<string> Post([FromBody] Address address)
        {
            using (var db = new InfoStudDB())
            {
                await db.Addresses.AddAsync(address);
                await db.SaveChangesAsync();
            }

            return "Added successfully";
        }

        [HttpPut]
        public async Task<string> Put(Address address)
        {
            using (var db = new InfoStudDB())
            {
                var editAddr = await db.Addresses.FindAsync(address.Id);

                if (editAddr != null)
                {
                    editAddr.City = address.City;
                    editAddr.PostIndex = address.PostIndex;
                    editAddr.Street = address.Street;

                    await db.SaveChangesAsync();
                }
                else
                {
                    return "Error! Failed to edit this date.";
                }
            }
            return "Edited successfully";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            using (var db = new InfoStudDB())
            {
                var address = await db.Addresses.FindAsync(id);

                if (address != null)
                {
                    db.Addresses.Remove(address);

                    await db.SaveChangesAsync();
                }
                else
                {
                    return "Error! Failed to delete this date.";
                }
            }
            return "Deleted successfully";
        }
    }
}
