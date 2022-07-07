using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Services;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;
using Xunit;

namespace UChi.CourseEvals.Api.Test.Services;

public class CoursesServiceTest
{
    private static readonly Section _dbSection = new Section()
    {
            Id = 1,
            Keywords = "[]",
            Quarter = Quarter.Autumn,
            Sentiment = 0,
            EnrolledCount = 1,
            Year = 2022,
            Number = 5,
            CourseId = 1,
    };

    private static readonly Course _dbCourse = new Course()
    {
        Id = 1,
        Sections = new List<Section>(),
        Title = "Test title 1",
        CourseNumbers = new List<CourseNumber>()
        {
            new CourseNumber()
            {
                Number = 16100,
                Department = "CMSC"
            }
        }
    };

    [Fact]
    public async Task RetainsCourseTitleIfGivenSectionNotMostRecent()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestAppDatabase1")
            .Options;

        await using (var dbContext = new AppDbContext(options))
        {
            dbContext.Courses.Add(_dbCourse);
            _dbCourse.Sections.Add(_dbSection);
            await dbContext.SaveChangesAsync();
        }

        await using (var dbContext = new AppDbContext(options))
        {
            var coursesService = new CoursesService(dbContext);
            await coursesService.UpdateCourseTitleToMostRecent(1, 2021, Quarter.Autumn, "Test title 2");
            var title = (await coursesService.FindById(1)).Title;

            Assert.Equal("Test title 1", title);
        }
    }

    [Fact]
    public async Task UpdatesCourseTitleIfGivenSectionIsMostRecent()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestAppDatabase2")
            .Options;

        await using (var dbContext = new AppDbContext(options))
        {
            dbContext.Courses.Add(_dbCourse);
            _dbCourse.Sections.Add(_dbSection);
            await dbContext.SaveChangesAsync();
        }

        await using (var dbContext = new AppDbContext(options))
        {
            var coursesService = new CoursesService(dbContext);
            await coursesService.UpdateCourseTitleToMostRecent(1, 2022, Quarter.Winter, "Test title 2");
            var title = (await coursesService.FindById(1)).Title;

            Assert.Equal("Test title 2", title);
        }
    }
}