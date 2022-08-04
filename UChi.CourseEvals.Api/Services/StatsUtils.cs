using UChi.CourseEvals.Api.Models.Stats;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services;

public static class StatsUtils
{
    public static StatsModel GetStatsForSections(List<Section> sections)
    {
        var averageSentiment = sections
            .Average(s => s.Sentiment);

        var sectionsWithHours = sections
            .Where(s => s.HoursWorked != null)
            .ToList();
        var hoursWorked = new StatComparison
        {
            SectionCount = sectionsWithHours.Count,
            Average = sectionsWithHours.Average(s => s.HoursWorked)
        };

        var sectionsWithUsefulFeedback = sections
            .Where(s => s.UsefulFeedback != null)
            .ToList();
        var usefulFeedback = new StatComparison
        {
            SectionCount = sectionsWithUsefulFeedback.Count,
            Average = sectionsWithUsefulFeedback.Average(s => s.UsefulFeedback)
        };

        var sectionsWithEvaluatedFairly = sections
            .Where(s => s.EvaluatedFairly != null)
            .ToList();
        var evaluatedFairly = new StatComparison
        {
            SectionCount = sectionsWithEvaluatedFairly.Count,
            Average = sectionsWithEvaluatedFairly.Average(s => s.EvaluatedFairly)
        };

        var sectionsWithStandardsForSuccess = sections
            .Where(s => s.StandardsForSuccess != null)
            .ToList();
        var standardsForSuccess = new StatComparison
        {
            SectionCount = sectionsWithStandardsForSuccess.Count,
            Average = sectionsWithStandardsForSuccess.Average(s => s.StandardsForSuccess)
        };

        var sectionsWithHelpfulOutsideOfClass = sections
            .Where(s => s.HelpfulOutsideOfClass != null)
            .ToList();
        var helpfulOutsideOfClass = new StatComparison
        {
            SectionCount = sectionsWithHelpfulOutsideOfClass.Count,
            Average = sectionsWithHelpfulOutsideOfClass.Average(s => s.HelpfulOutsideOfClass)
        };

        var model = new StatsModel
        {
            SectionCount = sections.Count,
            Sentiment = averageSentiment,
            EnrolledCount = sections.Sum(s => s.EnrolledCount),
            RespondentCount = sections.Sum(s => s.RespondentCount),
            EvaluatedFairly = evaluatedFairly,
            HoursWorked = hoursWorked,
            UsefulFeedback = usefulFeedback,
            StandardsForSuccess = standardsForSuccess,
            HelpfulOutsideOfClass = helpfulOutsideOfClass
        };

        return model;
    }
}