using CollegeManagement.Models;
using Microsoft.AspNetCore.Http;
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
                return BadRequest(new { msg = "Id should not be null" });

            var user = await _context.Studentdetails.FindAsync(id);

            if (user == null)
                return NotFound(new { msg = $"User not found with id {id}" });


            return Ok(user);
        }
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<Studentdetail>> UpdateStudent(int id, [FromBody] Studentdetail student)
        {
            if (true)
            {
                var studentdb = await _context.Studentdetails.FirstOrDefaultAsync(x => x.Id == id);
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
                return Ok();

            }
        }
    }
}

