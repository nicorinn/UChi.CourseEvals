using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Mapping;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Models.Stats;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services;

public class InstructorService : IInstructorService
{
    private readonly IAppDbContext _dbContext;

    public InstructorService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<InstructorModel?> FindById(int id)
    {
        var instructor = await _dbContext.Instructors
            .Include(i => i.Sections)
            .ThenInclude(s => s.Course)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (instructor != null)
        {
            instructor.Sections = instructor.Sections
                .OrderBy(s => s.Course?.Title)
                .ThenByDescending(s => s.Year)
                .ThenByDescending(s => s.Quarter)
                .ToList();
        }

        return instructor == null ? 
            null : Mapper.InstructorToInstructorModel(instructor);
    }

    public async Task<Instructor?> FindByName(string name)
    {
        return await _dbContext.Instructors
            .FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<IEnumerable<InstructorModel>> SearchInstructors(string queryString, int page, int pageSize)
    {
        var instructors = await _dbContext.Instructors
            .Where(i => 
                i.Name.ToLower().Contains(queryString.ToLower()))
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return instructors.ConvertAll(Mapper.InstructorToInstructorModel);
    }

    public async Task<int> GetInstructorSearchResultsLength(string queryString)
    {
        var count = await _dbContext.Instructors
            .Where(i =>
                i.Name.ToLower().Contains(queryString.ToLower()))
            .CountAsync();
        return count;
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

    public async Task<StatsModel> GetInstructorStats(int instructorId)
    {
        var instructor = await _dbContext.Instructors
            .Include(c => c.Sections)
            .FirstAsync(c => c.Id == instructorId);

        var model = StatsUtils.GetStatsForSections(instructor.Sections.ToList());

        return model;
    }
}