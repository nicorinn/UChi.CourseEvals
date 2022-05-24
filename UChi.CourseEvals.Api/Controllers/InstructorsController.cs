using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Services.Interfaces;

namespace UChi.CourseEvals.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Instructor(int id)
        {
            var instructor = await _instructorService.FindById(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return Ok(instructor);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Stats(int id)
        {
            var model = await _instructorService.GetInstructorStats(id);
            return Ok(model);
        }
    }
}