using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Services.Interfaces;

namespace UChi.CourseEvals.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
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
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Stats(int id)
    {
        var model = await _coursesService.GetCourseStats(id);
        return Ok(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery]string queryString, [FromQuery]int page, 
        [FromQuery]int pageSize)
    {
        var courses = await _coursesService
            .SearchByQueryString(queryString, page, pageSize);
        var response = new {
            Courses = courses,
            Count = await _coursesService.GetCourseSearchResultsLength(queryString),
        };
        return Ok(response);
    }
}