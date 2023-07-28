﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Address>> GetAddresses()
        {
            var addressList = new List<Address>();

            using (var db = new StudentContext())
            {
                addressList = await db.Addresses.Include(a => a.City).Include(a => a.Postindex).Include(a => a.Street).ToListAsync();
            }

            return addressList;
        }

        [HttpPost]
        public async Task<List<Address>> PostAddress([FromBody] Address address)
        {
            using (var db = new StudentContext())
            {
                await db.Addresses.AddAsync(address);
                await db.SaveChangesAsync();
            }

            return await GetAddresses();
        }

        [HttpPut]
        public async Task<List<Address>> PutAddress(Address address)
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

            return await GetAddresses();
        }

        [HttpDelete("{id}")]
        public async Task<List<Address>> DeleteAddress(long id)
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

            return await GetAddresses();
        }
    }
}
