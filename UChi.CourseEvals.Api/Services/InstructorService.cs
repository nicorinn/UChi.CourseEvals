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

    public async Task<int> GetInstructorsSearchResultsLength(string queryString)
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
        var course = await _dbContext.Instructors
            .Include(c => c.Sections)
            .FirstAsync(c => c.Id == instructorId);

        var averageSentiment = course.Sections
            .Average(s => s.Sentiment);

        var averageHours = course.Sections
            .Where(s => s.HoursWorked != null)
            .Average(s => s.HoursWorked);
        
        var usefulFeedback = course.Sections
            .Where(s => s.UsefulFeedback != null)
            .Average(s => s.UsefulFeedback);
        
        var evaluateFairly = course.Sections
            .Where(s => s.EvaluatedFairly != null)
            .Average(s => s.EvaluatedFairly);
        
        var standardForSuccess = course.Sections
            .Where(s => s.StandardsForSuccess != null)
            .Average(s => s.StandardsForSuccess);
        
        var helpfulOutsideOfClass = course.Sections
            .Where(s => s.HelpfulOutsideOfClass != null)
            .Average(s => s.HelpfulOutsideOfClass);

        var model = new StatsModel
        {
            SectionCount = course.Sections.Count,
            Sentiment = averageSentiment,
            EnrolledCount = course.Sections.Sum(s => s.EnrolledCount),
            RespondentCount = course.Sections.Sum(s => s.RespondentCount),
            EvaluatedFairly = evaluateFairly,
            HoursWorked = averageHours,
            UsefulFeedback = usefulFeedback,
            StandardsForSuccess = standardForSuccess,
            HelpfulOutsideOfClass = helpfulOutsideOfClass
        };
        
        return model;
    }
}