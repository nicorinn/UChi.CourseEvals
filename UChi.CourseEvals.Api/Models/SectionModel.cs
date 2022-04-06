using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Models;

public class SectionModel
{
    public SectionModel()
    {
        Instructors = new List<InstructorModel>();
    }
    
    public int Id { get; set; }
    public int Number { get; set; }
    public int Year { get; set; }
    public string Quarter { get; set; }
    public string? ChartData { get; set; }
    public double Sentiment { get; set; }
    
    public int? HoursWorked { get; set; }
    public bool IsVirtual { get; set; }
    public ICollection<InstructorModel> Instructors { get; set; }
}