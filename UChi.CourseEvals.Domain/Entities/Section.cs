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
    public string? ChartData { get; set; }
    public double Sentiment { get; set; }
    
    public int? HoursWorked { get; set; }
    public bool IsVirtual { get; set; }

    public Course? Course { get; set; }
    public ICollection<Instructor> Instructors { get; set; }
}