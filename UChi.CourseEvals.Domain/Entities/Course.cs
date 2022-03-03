using System.ComponentModel.DataAnnotations.Schema;
using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class Course : BaseEntity
{
    public Course()
    {
        Sections = new List<Section>();
    }
    
    public string Title { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int Number { get; set; }
    public double AverageSentiment { get; set; }
    [Column(TypeName = "jsonb")] 
    public string? ChartData { get; set; }

    public ICollection<Section> Sections { get; set; }
}