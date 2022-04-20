using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services.Interfaces;

public interface ICoursesService
{
    public Task<CourseModel?> FindById(int id);

    public Task<CourseModel?> FindCourseAndSectionsByNumber(string courseNumber);

    public Task<Course?> FindByCourseNumber(string courseNumber);
    public Task<IEnumerable<CourseModel>> SearchByQueryString(string queryString);

    public Task AddCourse(NewSectionModel sectionModel);
}