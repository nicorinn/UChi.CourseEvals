using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace UChi.CourseEvals.Api.Models;

public class NewSectionModel
{
    public NewSectionModel()
    {
        Instructors = new List<string>();
        CourseNumbers = new List<string>();
    }
    
    public int Number { get; set; }
    public int Year { get; set; }
    public string Quarter { get; set; } = string.Empty;
    public string? ChartData { get; set; }
    public double Sentiment { get; set; }
    [BindProperty(Name = "hours_worked")]
    public int? HoursWorked { get; set; }
    [BindProperty(Name = "is_virtual")]
    public bool IsVirtual { get; set; }
    public int EnrolledCount { get; set; }
    public int RespondentCount { get; set; }
    [BindProperty(Name = "title")]
    public string Title { get; set; } = string.Empty;
    public ICollection<string> CourseNumbers { get; set; }
    public ICollection<string> Instructors { get; set; }
}