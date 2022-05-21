using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services.Interfaces;

public interface IInstructorService
{
    public Task<InstructorModel?> FindById(int id);

    public Task<Instructor?> FindByName(string name);

    public Task<IEnumerable<InstructorModel?>> SearchInstructors(string query);

    public Task<Instructor> AddInstructor(string name);
}