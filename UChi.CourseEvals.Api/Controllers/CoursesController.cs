using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Services.Interfaces;

namespace UChi.CourseEvals.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;
    private readonly IInstructorService _instructorService;

    public CoursesController(ICoursesService coursesService, 
        IInstructorService instructorService)
    {
        _coursesService = coursesService;
        _instructorService = instructorService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Course(int id)
    {
        var course = await _coursesService.FindById(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }
}