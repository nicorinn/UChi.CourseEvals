namespace UChi.CourseEvals.Api.Models;

public class SaveSectionDTO
{
    public SaveSectionDTO()
    {
        Section = new NewSectionModel();
    }
    public string ApiKey { get; set; } = string.Empty;
    public NewSectionModel Section { get; set; }
}