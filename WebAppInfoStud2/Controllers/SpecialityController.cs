﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Context;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        [HttpGet]
        public async Task<List<SpecialityTable>> Get()
        {
            var specialityList = new List<SpecialityTable>();

            using (var db = new StudentContext())
            {
                specialityList = await db.SpecialityTables.ToListAsync();
            }
            return specialityList;
        }
    }
}
