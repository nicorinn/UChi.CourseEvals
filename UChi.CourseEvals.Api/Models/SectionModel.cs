using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Models;

public class SectionModel
{
    public SectionModel()
    {
        Keywords = new List<KeywordModel>();
        Instructors = new List<InstructorModel>();
    }
    
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int Number { get; set; }
    public int Year { get; set; }
    public string? Quarter { get; set; }
    public List<KeywordModel>? Keywords { get; set; }
    public double Sentiment { get; set; }
    public int EnrolledCount { get; set; }
    public int RespondentCount { get; set; }
    public bool IsVirtual { get; set; }
    public int? HoursWorked { get; set; }
    public double? UsefulFeedback { get; set; }
    public double? EvaluatedFairly { get; set; }
    public double? StandardsForSuccess { get; set; }
    public double? HelpfulOutsideOfClass { get; set; }
    public string? CourseTitle { get; set; } = string.Empty;
    public string? CourseNumber { get; set; } = string.Empty;
    public string? Url { get; set; } = string.Empty;
    public ICollection<InstructorModel> Instructors { get; set; }
}