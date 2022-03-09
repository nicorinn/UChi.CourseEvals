using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class CourseNumber : BaseEntity
{
    public int CourseId { get; set; }
    public string DepartmentAndNumber { get; set; } = string.Empty;
}