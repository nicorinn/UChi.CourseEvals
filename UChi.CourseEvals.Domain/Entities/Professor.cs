using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class Professor : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Section>? Sections { get; set; }
}