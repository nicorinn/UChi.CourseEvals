using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using UChi.CourseEvals.Api.Mapping;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services;

public class CoursesService : ICoursesService
{
    private readonly AppDbContext _dbContext;

    public CoursesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CourseModel?> FindById(int id)
    {
        var course = await _dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == id);
        if (course == null)
        {
            return null;
        }
        course.Sections = await _dbContext.Sections
            .Include(s => s.Instructors)
            .Where(s => s.CourseId == course.Id)
            .ToListAsync();

        return Mapper.CourseToCourseModel(course);
    }

    public async Task<CourseModel?> FindCourseAndSectionsByNumber(string courseNumber)
    {
        var course = await FindByCourseNumber(courseNumber);
        if (course == null)
        {
            return null;
        }
        course.Sections = await _dbContext.Sections
            .Include(s => s.Instructors)
            .Where(s => s.CourseId == course.Id)
            .ToListAsync();

        return Mapper.CourseToCourseModel(course);
    }

    public async Task<Course?> FindByCourseNumber(string courseNumber)
    {
        var course = await _dbContext.Courses
            .Include(c => c.CourseNumbers)
            .FirstOrDefaultAsync(c => c.HasCourseNumber(courseNumber));

        return course;
    }

    public async Task AddCourse(NewSectionModel sectionModel)
    {
        var course = new Course
        {
            Sections = new List<Section>(),
            Title = sectionModel.Title,
            CourseNumbers = new List<CourseNumber>()
        };

        _dbContext.Courses.Add(course);

        foreach (var courseNumberString in sectionModel.CourseNumbers)
        {
            var newCourseNumber = new CourseNumber(courseNumberString, course.Id);
            _dbContext.CourseNumbers.Add(newCourseNumber);
        }

        await _dbContext.SaveChangesAsync();
    }
}