using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class SectionsController : ControllerBase
{
    private readonly ISectionsService _sectionsService;
    private readonly IApiKeyService _apiKeyService;

    public SectionsController(ISectionsService sectionsService,
        IApiKeyService apiKeyService)
    {
        _sectionsService = sectionsService;
        _apiKeyService = apiKeyService;
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

    [HttpPost(Name = "section")]
    public async Task<IActionResult> Section([FromBody] SaveSectionDTO data)
    {
        var apiKeyValid = await _apiKeyService
            .IsApiKeyValid(data.ApiKey, ApiKeyScope.Write);
        if (!apiKeyValid)
        {
            return Unauthorized(new { message = "Invalid API key"});
        }
        var newSection = await _sectionsService.AddSection(data.Section);
        if (newSection == null)
        {
            return BadRequest();
        }
        await _apiKeyService.TrackApiKeyRequest(data.ApiKey);
        return Ok(newSection);
    }
}