using System.ComponentModel.DataAnnotations.Schema;
using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class ApiKey: BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreationDate { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ExpirationDate { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? LastUsed { get; set; }
    public long RequestCount { get; set; }
}