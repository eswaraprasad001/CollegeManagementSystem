using CollegeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;
namespace CollegeManagement.Controllers
{
    [Route("api/[controller]")]
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
            return Ok("Staff Created Successfully");
        }
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult<Studentdetail>> AddStudent([FromBody] Studentdetail student)
        {

            _context.Studentdetails.Add(student);
            await _context.SaveChangesAsync();
            return Ok("Student Created Successfully");
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
                    userdb.Password = bcrypt.HashPassword(user.Password, 12); 
                    await _context.SaveChangesAsync();
                    return Ok(userdb);

                }
                return Ok("Error while updating Student Profile data");

            }

        }



    }
}
