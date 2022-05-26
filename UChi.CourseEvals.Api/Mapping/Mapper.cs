using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;
namespace UChi.CourseEvals.Api.Mapping;
using System.Text.Json;

public static class Mapper
{
    public static Section NewSectionModelToSection(NewSectionModel sectionModel)
    {
        var section = new Section
        {
            Number = sectionModel.Number,
            Quarter = StringToQuarter(sectionModel.Quarter),
            Year = sectionModel.Year,
            Sentiment = sectionModel.Sentiment,
            EnrolledCount = sectionModel.EnrolledCount,
            RespondentCount = sectionModel.RespondentCount,
            IsVirtual = sectionModel.IsVirtual,
            HoursWorked = sectionModel.HoursWorked,
            EvaluatedFairly = sectionModel.EvaluatedFairly,
            UsefulFeedback = sectionModel.UsefulFeedback,
            StandardsForSuccess = sectionModel.StandardsForSuccess,
            HelpfulOutsideOfClass = sectionModel.HelpfulOutsideOfClass,
            Keywords = JsonSerializer.Serialize(sectionModel.Keywords)
        };
        return section;
    }

    public static SectionModel SectionToSectionModel(Section section)
    {
        var sectionModel = new SectionModel
        {
            Id = section.Id,
            CourseId = section.CourseId,
            Number = section.Number,
            Quarter = QuarterToString(section.Quarter),
            Year = section.Year,
            Sentiment = section.Sentiment,
            HoursWorked = section.HoursWorked,
            IsVirtual = section.IsVirtual,
            EnrolledCount = section.EnrolledCount,
            RespondentCount = section.RespondentCount,
            Keywords = JsonSerializer.Deserialize<List<KeywordModel>>(section.Keywords),
            EvaluatedFairly = section.EvaluatedFairly,
            UsefulFeedback = section.UsefulFeedback,
            StandardsForSuccess = section.StandardsForSuccess,
            HelpfulOutsideOfClass = section.HelpfulOutsideOfClass,
            CourseTitle = section.Course?.Title,
            Instructors = section.Instructors.Select(i => new InstructorModel
            {
                Id = i.Id,
                Name = i.Name
            }).ToList()
        };
        return sectionModel;
    }

    public static CourseModel CourseToCourseModel(Course course)
    {
        var courseModel = new CourseModel
        {
            Id = course.Id,
            Title = course.Title,
            CourseNumbers = course.CourseNumbers
                .Select(cn => cn.GetDepartmentAndNumber()).ToList(),
            Sections = course.Sections.Select(SectionToSectionModel).ToList(),
        };
        return courseModel;
    }

    public static InstructorModel InstructorToInstructorModel(Instructor instructor)
    {
        var instructorModel = new InstructorModel
        {
            Id = instructor.Id,
            Name = instructor.Name,
            Sections = instructor.Sections.Select(SectionToSectionModel).ToList()
        };

        return instructorModel;
    }

    private static Quarter StringToQuarter(string quarterString)
    {
        return quarterString switch
        {
            "Autumn" => Quarter.Autumn,
            "Winter" => Quarter.Winter,
            "Spring" => Quarter.Spring,
            "Summer" => Quarter.Summer,
            _ => throw new Exception("Invalid quarter")
        };
    }

    private static string QuarterToString(Quarter quarter)
    {
        return quarter switch
        {
            Quarter.Autumn => "Autumn",
            Quarter.Winter => "Winter",
            Quarter.Spring => "Spring",
            Quarter.Summer => "Summer",
            _ => throw new ArgumentOutOfRangeException(nameof(quarter), quarter, null)
        };
    }
}