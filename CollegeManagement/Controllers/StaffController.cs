using CollegeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "1")]
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
            {
                return BadRequest(new { msg = "Id should not be null" });
            }

            var user = await _context.Staffdetails.FirstOrDefaultAsync(x => x.Staffid == id);

            if (user == null)
            {
                return NotFound(new { msg = $"User not found with id {id} or Please wait until the data is updated by the admin" });
            }

            return Ok(user);
        }
        [HttpPut]
        [Route("[Action]/{staffid}")]
        public async Task<ActionResult<Staffdetail>> UpdateStaffDetails(int staffid, [FromBody] Staffdetail staff)
        {
            if (true)
            {
                var staffdb = await _context.Staffdetails.FirstOrDefaultAsync(x => x.Staffid == staffid);
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

        [HttpPut]
        [Route("[Action]/{staffid}")]
        public async Task<ActionResult<User>> UpdateStaffProfile(int staffid, [FromBody] User user)
        {
            if (true)
            {
                var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == staffid);
                if (userdb != null)
                {

                    userdb.Name = user.Name;

                    userdb.Email = user.Email;
                    userdb.Password = user.Password;
                    await _context.SaveChangesAsync();
                    return Ok(userdb);

                }
                return Ok();

            }

        }
    }
}
