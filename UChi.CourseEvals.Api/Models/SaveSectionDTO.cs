namespace UChi.CourseEvals.Api.Models;

public class SaveSectionDTO
{
    public string ApiKey { get; set; } = string.Empty;
    public NewSectionModel Section { get; set; }
}