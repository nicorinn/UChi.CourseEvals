using System.ComponentModel.DataAnnotations;

namespace UChi.CourseEvals.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}