using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class Instructor : BaseEntity
{
    public Instructor()
    {
        Sections = new List<Section>();
    }

    public Instructor(string name)
    {
        Sections = new List<Section>();
        Name = name;
    }

    public string Name { get; set; } = string.Empty;

    public ICollection<Section> Sections { get; set; }
}