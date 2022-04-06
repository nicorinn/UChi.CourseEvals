using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // GET: api/Home
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _dbContext.Courses
                .Include(c => c.Sections)
                .ThenInclude(s => s.Instructors)
                .ToListAsync();
            return Ok(courses);
        }

        // GET: api/Home/Instructors/5
        [HttpGet("{id}", Name = "Instructors")]
        public async Task<IActionResult> Instructors(int id)
        {
            var prof = await _dbContext.FindAsync<Instructor>(id);
            if (prof == null)
            {
                return NotFound();
            }
            return Ok(prof);
        }
    }
}