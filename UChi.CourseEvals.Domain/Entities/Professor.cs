using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class Professor : BaseEntity
{
    public Professor()
    {
        Sections = new List<Section>();
    }

    public string Name { get; set; } = string.Empty;

    public ICollection<Section> Sections { get; set; }
}