﻿using CollegeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly collegemanagementContext _context;

        public StaffController(collegemanagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<List<Staffdetail>>> Index()
        {
            return Ok(_context.Staffdetails.ToList<Staffdetail>());
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<Object>> GetStaff(int id)
        {
            if (id == null || _context?.Staffdetails == null)
                return BadRequest(new { msg = "Id should not be null" });

            var user = await _context.Staffdetails.FindAsync(id);

            if (user == null)
                return NotFound(new { msg = $"User not found with id {id}" });


            return Ok(user);
        }
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<Staffdetail>> UpdateStaff(int id, [FromBody] Staffdetail staff)
        {
            if (true)
            {
                var staffdb = await _context.Staffdetails.FirstOrDefaultAsync(x => x.Staffid == id);
                if (staffdb != null)
                {

                    staffdb.Staffname = staff.Staffname;

                    staffdb.Stream = staff.Stream;
                    staffdb.Doj = staff.Doj;
                    staffdb.Address = staff.Address;
                    staffdb.Designation = staff.Designation;
                    await _context.SaveChangesAsync();
                    return Ok(staffdb);

                }
                return Ok();

            }
        }
    }
}