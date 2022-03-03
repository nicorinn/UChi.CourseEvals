using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UChi.CourseEvals.Domain.Common;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Domain.Entities;

public class Section : BaseEntity
{
    public int CourseId { get; set; }
    public int ProfessorId { get; set; }
    public int Number { get; set; }
    public int Year { get; set; }
    public Quarter Quarter { get; set; }
    [Column(TypeName = "jsonb")]
    public string? ChartData { get; set; }
    public double Sentiment { get; set; }

    public Course? Course { get; set; }
    public Professor? Professor { get; set; }
}