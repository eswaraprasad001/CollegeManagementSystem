using CollegeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly collegemanagementContext _context;
        public StudentController(collegemanagementContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<List<Studentdetail>>> Index()
        {
            return Ok(_context.Studentdetails.ToList<Studentdetail>());
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<Object>> GetStudent(int id)
        {
            if (id == null || _context?.Studentdetails == null)
            {
                return BadRequest(new { msg = "Id should not be null" });
            }

            var student = await _context.Studentdetails.FirstOrDefaultAsync(x=>x.Studentid==id);

            if (student == null)
            {
                return NotFound(new { msg = $"User not found with id {id}" });
            }

            return Ok(student);
        }
        [HttpPut]
        [Route("[Action]/{studentid}")]
        public async Task<ActionResult<Studentdetail>> UpdateStudentDetails(int studentid, [FromBody] Studentdetail student)
        {
            if (true)
            {
                var studentdb = await _context.Studentdetails.FirstOrDefaultAsync(x => x.Studentid == studentid);
                if (studentdb != null)
                {

                    studentdb.Studentname = student.Studentname;

                    studentdb.Department = student.Department;
                    studentdb.Address = student.Address;
                    studentdb.Phoneno = student.Phoneno;
                    studentdb.Batch = student.Batch;
                    await _context.SaveChangesAsync();
                    return Ok(studentdb);

                }
                return Ok("Error while Saving Students Details");

            }
        }


        [HttpPut]
        [Route("[Action]/{studentid}")]
        public async Task<ActionResult<User>> UpdateStudentProfile(int studentid, [FromBody] User user)
        {
            if (true)
            {
                var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == studentid);
                if (userdb != null)
                {

                    userdb.Name = user.Name;

                    userdb.Email = user.Email;
                    userdb.Password = user.Password;
                    await _context.SaveChangesAsync();
                    return Ok(userdb);

                }
                return Ok("Error while updating Student Profile data");

            }

        }
    }
}

