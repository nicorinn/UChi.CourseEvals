namespace UChi.CourseEvals.Api.Models;

public class StatsModel
{
    public StatsModel()
    {
        
    }
    public int SectionCount { get; set; }
    public double Sentiment { get; set; }
    public int EnrolledCount { get; set; }
    public int RespondentCount { get; set; }
    public double? HoursWorked { get; set; }
    public double? UsefulFeedback { get; set; }
    public double? EvaluatedFairly { get; set; }
    public double? StandardsForSuccess { get; set; }
    public double? HelpfulOutsideOfClass { get; set; }
}