using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Mapping;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services;

public class SectionsService : ISectionsService
{
    private readonly AppDbContext _dbContext;
    private readonly ICoursesService _coursesService;
    private readonly IInstructorService _instructorService;

    public SectionsService(AppDbContext dbContext, 
        ICoursesService coursesService,
        IInstructorService instructorService)
    {
        _dbContext = dbContext;
        _coursesService = coursesService;
        _instructorService = instructorService;
    }

    public async Task<SectionModel?> GetSectionById(int id)
    {
        var section = await _dbContext.Sections
            .Include(s => s.Instructors)
            .FirstOrDefaultAsync(s => s.Id == id);
        return section == null ? null : Mapper.SectionToSectionModel(section);
    }

    public async Task<Section?> AddSection(NewSectionModel sectionModel)
    {
        var course = await _coursesService
            .FindByCourseNumber(sectionModel.CourseNumbers.First());

        if (course == null)
        {
           await _coursesService.AddCourse(sectionModel);
           course = await _coursesService
               .FindByCourseNumber(sectionModel.CourseNumbers.First());
        }

        var newSection = Mapper.NewSectionModelToSection(sectionModel);
        newSection.CourseId = course.Id;
        await AddInstructorsToSection(newSection, sectionModel.Instructors);
        _dbContext.Add(newSection);

        await _dbContext.SaveChangesAsync();
        return await _dbContext.Sections.FindAsync(newSection);
    }

    private async Task AddInstructorsToSection(Section section, IEnumerable<string> names)
    {
        foreach (string name in names)
        {
            var instructor = await _instructorService.FindByName(name);
            if (instructor != null)
            {
                section.Instructors.Add(instructor);
            }
            else
            {
                var newInstructor = await _instructorService.AddInstructor(name);
                section.Instructors.Add(newInstructor);
            }
        }
    }

    private bool SameSection(Section section1, Section section2)
    {
        bool sameQuarter = section1.Quarter == section2.Quarter 
                           && section1.Year == section2.Year;
        bool sameNumber = section1.Number == section2.Number;
        bool sameCourse = section1.CourseId == section2.CourseId;
        return sameCourse && sameQuarter && sameNumber;
    }
}