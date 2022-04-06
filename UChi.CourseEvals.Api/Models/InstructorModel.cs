namespace UChi.CourseEvals.Api.Models;

public class InstructorModel
{
    public InstructorModel()
    {
        Sections = new List<SectionModel>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<SectionModel> Sections { get; set; }
}