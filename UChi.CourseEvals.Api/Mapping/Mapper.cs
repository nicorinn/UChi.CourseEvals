using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Mapping;

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
            HoursWorked = sectionModel.HoursWorked,
            IsVirtual = sectionModel.IsVirtual,
            ChartData = sectionModel.ChartData
        };
        return section;
    }

    public static SectionModel SectionToSectionModel(Section section)
    {
        var sectionModel = new SectionModel
        {
            Number = section.Number,
            Quarter = QuarterToString(section.Quarter),
            Year = section.Year,
            Sentiment = section.Sentiment,
            HoursWorked = section.HoursWorked,
            IsVirtual = section.IsVirtual,
            ChartData = section.ChartData,
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

    private static Quarter StringToQuarter(string quarterString)
    {
        return quarterString switch
        {
            "Autumn" => Quarter.Autumn,
            "Winter" => Quarter.Winter,
            "Spring" => Quarter.Spring,
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
            _ => throw new ArgumentOutOfRangeException(nameof(quarter), quarter, null)
        };
    }
}