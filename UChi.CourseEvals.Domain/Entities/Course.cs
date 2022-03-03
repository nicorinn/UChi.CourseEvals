using System.ComponentModel.DataAnnotations.Schema;
using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public decimal AverageSentiment { get; set; }
    [Column(TypeName = "jsonb")]
    public string? ChartData { get; set; }
    public ICollection<Section>? Sections { get; set; }
}