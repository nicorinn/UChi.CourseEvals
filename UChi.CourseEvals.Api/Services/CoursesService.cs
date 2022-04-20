using System.Linq;
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

    // TODO Investigate whether to query by coursenumber first
    public async Task<Course?> FindByCourseNumber(string courseNumber)
    {
        var splitCourseNumber = courseNumber.Split(' ');
        if (splitCourseNumber.Length < 2)
        {
            return null;
        }
        var department = splitCourseNumber[0];
        var number = splitCourseNumber[1];
        var course = await _dbContext.Courses
            .Include(c => c.CourseNumbers)
            .Include(c => c.Sections)
            .FirstOrDefaultAsync(c => 
                c.CourseNumbers.Any(cn => 
                    cn.Department == department && cn.Number.ToString() == number));

        return course;
    }

    // TODO Improve search
    public async Task<IEnumerable<CourseModel>> SearchByQueryString(string queryString)
    {
        // var searchTerm = queryString.ToLower();
        // var results = await _dbContext.Courses
        //     .Include(c => c.CourseNumbers)
        //     .Include(c => c.Sections)
        //     .ThenInclude(c => c.Instructors)
        //     .Where(c => 
        //         c.Title.ToLower().Contains(searchTerm)
        //         || c.Sections.Any(s => s.Instructors
        //             .Any(i => i.Name.ToLower().Contains(searchTerm)))
        //         || c.CourseNumbers.Contains()
        //     )
        //     .ToListAsync();
        return default;
    }

    public async Task AddCourse(NewSectionModel sectionModel)
    {
        var course = new Course
        {
            Sections = new List<Section>(),
            Title = sectionModel.Title,
            CourseNumbers = new List<CourseNumber>()
        };

        course.CourseNumbers = sectionModel.CourseNumbers
            .Select(courseNumberString => new CourseNumber(courseNumberString))
            .ToList();

        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync();
    }
}