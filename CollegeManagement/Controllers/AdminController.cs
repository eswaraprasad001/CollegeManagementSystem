using CollegeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;
namespace CollegeManagement.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "0")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly collegemanagementContext _context;
        public AdminController(collegemanagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<List<Studentdetail>>> GetAllStudents()
        {
            return Ok(_context.Studentdetails.ToList<Studentdetail>());
        }
        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<List<Staffdetail>>> GetAllStaffs()
        {
            return Ok(_context.Staffdetails.ToList<Staffdetail>());
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(_context.Users.ToList<User>());
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult<Staffdetail>> AddStaff([FromBody] Staffdetail staff)
        {

            _context.Staffdetails.Add(staff);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Staff Created Successfully" });
        }
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult<Studentdetail>> AddStudent([FromBody] Studentdetail student)
        {

            _context.Studentdetails.Add(student);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Student Created Successfully" });
        }

        [HttpPut]
        [Route("[Action]/{Id}")]
        public async Task<ActionResult<User>> UpdateUser(int Id, [FromBody] User user)
        {
            if (true)
            {
                var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
                if (userdb != null)
                {
                   
                    userdb.Name = user.Name;
                    userdb.Status=user.Status;
                    userdb.Isadmin = user.Isadmin;                   
                    userdb.Email = user.Email;
                    userdb.Password = user.Password; 
                    await _context.SaveChangesAsync();
                    return Ok(userdb);

                }
                return Ok(new { msg = "Error while updating Student Profile data" });

            }

        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (id == null || _context?.Users == null)
            {
                return BadRequest(new { msg = "Id should not be null" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new { msg = $"User not found with id {id}" });
            }

            return Ok(user);
        }

        [HttpDelete]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<Object>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null )
                return BadRequest("User not found.");


            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }



    }
}
