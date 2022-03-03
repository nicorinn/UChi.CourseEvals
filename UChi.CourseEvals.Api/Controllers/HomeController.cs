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
            var courses = await _dbContext.Courses.ToListAsync();
            return Ok(courses);
        }
        
        [HttpGet("{id}", Name = "Course")]
        public async Task<IActionResult> Course(int id)
        {
            var course = await _dbContext
                .Courses
                .Include(course => course.Sections)
                .FirstOrDefaultAsync(course => course.Id == id);

            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        // GET: api/Home/Professors/5
        [HttpGet("{id}", Name = "Professors")]
        public async Task<IActionResult> Professors(int id)
        {
            var prof = await _dbContext.FindAsync<Professor>(id);
            if (prof == null)
            {
                return NotFound();
            }
            return Ok(prof);
        }
    }
}