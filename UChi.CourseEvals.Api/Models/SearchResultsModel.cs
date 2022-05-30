namespace UChi.CourseEvals.Api.Models;

public class SearchResultsModel
{
    public SearchResultsModel()
    {
        Courses = new List<CourseModel>();
        Instructors = new List<InstructorModel>();
    }

    public int CourseResultsCount { get; set; }
    public int InstructorResultsCount { get; set; }
    public IEnumerable<CourseModel> Courses { get; set; }
    public IEnumerable<InstructorModel> Instructors { get; set; }
}