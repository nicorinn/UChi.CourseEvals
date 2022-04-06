using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;

namespace UChi.CourseEvals.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SectionsController : ControllerBase
{
    private readonly ISectionsService _sectionsService;

    public SectionsController(ISectionsService sectionsService)
    {
        _sectionsService = sectionsService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Section(int id)
    {
        var section = await _sectionsService.GetSectionById(id);
        if (section == null)
        {
            return NotFound();
        }

        return Ok(section);
    }

    [HttpPost]
    public async Task<IActionResult> Section([FromBody] NewSectionModel sectionModel)
    {
        return Ok(sectionModel);
    }
}