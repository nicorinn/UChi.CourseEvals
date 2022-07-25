using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UChi.CourseEvals.Domain.Common;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Domain.Entities;

public class Section : BaseEntity
{
    public Section()
    {
        Instructors = new List<Instructor>();
    }
    
    public int CourseId { get; set; }
    public int Number { get; set; }
    public int Year { get; set; }
    public Quarter Quarter { get; set; }
    [Column(TypeName = "jsonb")]
    public string Keywords { get; set; }
    public double Sentiment { get; set; }
    public int EnrolledCount { get; set; }
    public int RespondentCount { get; set; }
    public bool IsVirtual { get; set; }
    public int? HoursWorked { get; set; }
    public double? UsefulFeedback { get; set; }
    public double? EvaluatedFairly { get; set; }
    public double? StandardsForSuccess { get; set; }
    public double? HelpfulOutsideOfClass { get; set; }
    public string? Url { get; set; }
    public Course? Course { get; set; }
    public ICollection<Instructor> Instructors { get; set; }
}