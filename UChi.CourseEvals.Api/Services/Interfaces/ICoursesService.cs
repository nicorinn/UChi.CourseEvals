using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Services.Interfaces;

public interface ICoursesService
{
    public Task<CourseModel?> FindById(int id);

    public Task<CourseModel?> FindCourseAndSectionsByNumber(string courseNumber);

    public Task<Course?> FindByCourseNumber(string courseNumber);
    
    public Task<IEnumerable<CourseModel>> SearchByQueryString(string queryString);

    public Task<Course> AddCourse(NewSectionModel sectionModel);

    public Task UpdateCourseTitleIfMoreRecent(Course course, int year, Quarter quarter, string newTitle);

    // public Task GetCourseStats(int courseId);
}