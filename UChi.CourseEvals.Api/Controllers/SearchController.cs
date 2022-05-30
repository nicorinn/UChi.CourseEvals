using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UChi.CourseEvals.Api.Models;
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
        public async Task<IActionResult> SearchEvals([FromQuery]string queryString, [FromQuery]int page, 
            [FromQuery]int pageSize)
        {
            var courses = await _coursesService
                .SearchByQueryString(queryString, page, pageSize);
            var instructors = await _instructorService
                .SearchInstructors(queryString, page, pageSize);
            var response = new SearchResultsModel
            {
                Courses = courses,
                Instructors = instructors,
                CourseResultsCount = await _coursesService.GetCoursesSearchResultsLength(queryString),
                InstructorResultsCount = await _instructorService.GetInstructorsSearchResultsLength(queryString)
            };
            return Ok(response);
        }
    }
}