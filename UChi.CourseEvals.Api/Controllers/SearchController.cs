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
    public class SearchController : ControllerBase
    {
        private readonly ICoursesService _coursesService;
        private readonly IInstructorService _instructorService;

        public SearchController(ICoursesService coursesService, 
            IInstructorService instructorService)
        {
            _coursesService = coursesService;
            _instructorService = instructorService;
        }
        
        [HttpGet]
        public async Task<IActionResult> SearchEvals([FromQuery]string queryString)
        {
            var courses = await _coursesService.SearchByQueryString(queryString);
            var instructors = await _instructorService.SearchInstructors(queryString);
            object response = new {courses, instructors};
            return Ok(response);
        }
    }
}