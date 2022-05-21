using System.Linq;
using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Mapping;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services;

public class InstructorService : IInstructorService
{
    private readonly AppDbContext _dbContext;

    public InstructorService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<InstructorModel?> FindById(int id)
    {
        var instructor = await _dbContext.Instructors
            .FirstOrDefaultAsync(i => i.Id == id);

        return instructor == null ? 
            null : Mapper.InstructorToInstructorModel(instructor);
    }

    public async Task<Instructor?> FindByName(string name)
    {
        return await _dbContext.Instructors
            .FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<IEnumerable<InstructorModel?>> SearchInstructors(string query)
    {
        var instructors = await _dbContext.Instructors
            .Where(i => 
                i.Name.ToLower().Contains(query.ToLower()))
            .ToListAsync();
        return instructors.ConvertAll(Mapper.InstructorToInstructorModel);
    }

    public async Task<Instructor> AddInstructor(string name)
    {
        var instructor = new Instructor
        {
            Name = name,
            Sections = new List<Section>()
        };
        _dbContext.Instructors.Add(instructor);
        await _dbContext.SaveChangesAsync();
        return instructor;
    }
}