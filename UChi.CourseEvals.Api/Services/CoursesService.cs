using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using UChi.CourseEvals.Api.Mapping;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;

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
            .Include(c => c.CourseNumbers)
            .Include(c => c.Sections)
            .ThenInclude(s => s.Instructors)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (course != null)
        {
            course.Sections = course.Sections
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Quarter)
                .ToList();
        }

        return course == null ? null : Mapper.CourseToCourseModel(course);
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
    public async Task<Course?> FindByCourseNumber(string courseNumberString)
    {
        var courseNumber = await FindCourseNumberByString(courseNumberString);
        if (courseNumber == null)
        {
            return null;
        }

        return courseNumber.Course;
    }

    public async Task<IEnumerable<CourseModel>> SearchByQueryString(string queryString)
    {
        var lowerQuery = queryString.ToLower();
        var courses = await _dbContext.Courses
            .Include(c => c.CourseNumbers)
            .Where(c =>
                c.Title.ToLower().Contains(lowerQuery)
                || c.CourseNumbers.Any(cn =>
                    (cn.Department + " " + cn.Number).ToLower().Contains(lowerQuery))
            ).ToListAsync();
        return courses.ConvertAll(Mapper.CourseToCourseModel);
    }

    public async Task<Course> AddCourse(NewSectionModel sectionModel)
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
        return course;
    }

    public async Task UpdateCourseTitleIfMoreRecent(Course course, int year, Quarter quarter, string newTitle)
    {
        var hasNewerSection = course.Sections
            .Any(s => s.Year >= year && s.Quarter > quarter);

        if (!hasNewerSection)
        {
            course.Title = newTitle;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<StatsModel> GetCourseStats(int courseId)
    {
        var course = await _dbContext.Courses
            .Include(c => c.Sections)
            .FirstAsync(c => c.Id == courseId);

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

    private async Task<CourseNumber?> FindCourseNumberByString(string courseNumberString)
    {
        var splitCourseNumber = courseNumberString.Split(' ');
        // There should never be a situation in which an argument without spaces is passed
        if (splitCourseNumber.Length != 2)
        {
            throw new ArgumentException("CourseNumber must contain department and number separated by a space.");
        }
        string department = splitCourseNumber[0];
        int number = int.Parse(splitCourseNumber[1]);
        var courseNumber = await _dbContext.CourseNumbers
            .Include(cn => cn.Course)
            .ThenInclude(c => c.Sections)
            .FirstOrDefaultAsync(cn => 
                cn.Department == department && cn.Number == number);
        return courseNumber;
    }

    private async Task AddCourseNumbers(NewSectionModel sectionModel, Course course)
    {
        foreach (var courseNumberString in sectionModel.CourseNumbers)
        {
            var courseNumber = await FindCourseNumberByString(courseNumberString);

            if (courseNumber == null)
            {
                courseNumber = new CourseNumber(courseNumberString);
            }
            course.CourseNumbers.Add(courseNumber);
        }
    }
}