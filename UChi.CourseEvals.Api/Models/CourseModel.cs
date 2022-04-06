namespace UChi.CourseEvals.Api.Models;

public class CourseModel
{
    public CourseModel()
    {
        Sections = new List<SectionModel>();
        CourseNumbers = new List<string>();
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ICollection<string> CourseNumbers { get; set; }
    public ICollection<SectionModel> Sections { get; set; }
}