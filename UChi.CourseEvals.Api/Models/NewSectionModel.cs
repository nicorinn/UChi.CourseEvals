using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace UChi.CourseEvals.Api.Models;

public class NewSectionModel
{
    public NewSectionModel()
    {
        Keywords = new List<KeywordModel>();
        Instructors = new List<string>();
        CourseNumbers = new List<string>();
    }
    public int Number { get; set; }
    public int Year { get; set; }
    public string Quarter { get; set; } = string.Empty;
    public double Sentiment { get; set; }
    public bool IsVirtual { get; set; }
    public int EnrolledCount { get; set; }
    public int RespondentCount { get; set; }
    public string Title { get; set; } = string.Empty;
    public double? UsefulFeedback { get; set; }
    public double? EvaluatedFairly { get; set; }
    public double? StandardsForSuccess { get; set; }
    public double? HelpfulOutsideOfClass { get; set; }
    public int? HoursWorked { get; set; }
    public List<KeywordModel> Keywords { get; set; }
    public ICollection<string> CourseNumbers { get; set; }
    public ICollection<string> Instructors { get; set; }

}