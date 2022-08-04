namespace UChi.CourseEvals.Api.Models.Stats;

public class StatsModel
{
    public StatsModel()
    {
        
    }
    public int SectionCount { get; set; }
    public double Sentiment { get; set; }
    public int EnrolledCount { get; set; }
    public int RespondentCount { get; set; }
    public StatComparison HoursWorked { get; set; }
    public StatComparison UsefulFeedback { get; set; }
    public StatComparison EvaluatedFairly { get; set; }
    public StatComparison StandardsForSuccess { get; set; }
    public StatComparison HelpfulOutsideOfClass { get; set; }
}