using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Mapping;
using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services;

public class SectionsService : ISectionsService
{
    private readonly IAppDbContext _dbContext;
    private readonly ICoursesService _coursesService;
    private readonly IInstructorService _instructorService;

    public SectionsService(IAppDbContext dbContext,
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
        var newSection = Mapper.NewSectionModelToSection(sectionModel);

        var course = await _coursesService
            .FindByCourseNumber(sectionModel.CourseNumbers.First());

        if (course == null)
        {
            var newCourse = await _coursesService.AddCourse(sectionModel);
            newSection.CourseId = newCourse.Id;
        }
        else
        {
            // Only check if course title needs to be updated when the course already exists
            await _coursesService.UpdateCourseTitleToMostRecent(course.Id, newSection.Year, newSection.Quarter,
                sectionModel.Title);
            newSection.CourseId = course.Id;

            var existingSection = course.Sections
                .FirstOrDefault(s => SameSection(s, newSection));
            // Update the existing section if the 
            if (existingSection != null)
            {
                var updatedSection = UpdateSection(existingSection, newSection);
                await _dbContext.SaveChangesAsync();
                return updatedSection;
            }
        }

        await AddInstructorsToSection(newSection, sectionModel.Instructors);
        _dbContext.Sections.Add(newSection);

        await _dbContext.SaveChangesAsync();
        return newSection;
    }

    private async Task AddInstructorsToSection(Section section, IEnumerable<string> names)
    {
        foreach (string name in names)
        {
            var instructor = await _instructorService.FindByName(name) ?? new Instructor
            {
                Name = name,
                Sections = new List<Section>()
            };
            section.Instructors.Add(instructor);
        }
    }

    private Section UpdateSection(Section sectionToUpdate, Section newSection)
    {
        // Only update properties that might change, so not number or quarter
        sectionToUpdate.Keywords = newSection.Keywords;
        sectionToUpdate.Sentiment = newSection.Sentiment;
        sectionToUpdate.EnrolledCount = newSection.EnrolledCount;
        sectionToUpdate.RespondentCount = newSection.RespondentCount;
        sectionToUpdate.EvaluatedFairly = newSection.EnrolledCount;
        sectionToUpdate.HoursWorked = newSection.HoursWorked;
        sectionToUpdate.UsefulFeedback = newSection.UsefulFeedback;
        sectionToUpdate.StandardsForSuccess = newSection.StandardsForSuccess;
        sectionToUpdate.HelpfulOutsideOfClass = newSection.EvaluatedFairly;
        return sectionToUpdate;
    }

    private bool SectionAlreadyExists(Section section, Course course)
    {
        return course.Sections
            .Any(s => SameSection(s, section));
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