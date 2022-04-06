using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Services.Interfaces;

namespace UChi.CourseEvals.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }
    
    [HttpGet("{id:int}", Name = "Course")]
    public async Task<IActionResult> Course(int id)
    {
        var course = await _coursesService.FindById(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpGet("{courseNumber:alpha}")]
    public async Task<IActionResult> GetCourseByNumber(string courseNumber)
    {
        var course = await _coursesService.FindByCourseNumber(courseNumber);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }
}