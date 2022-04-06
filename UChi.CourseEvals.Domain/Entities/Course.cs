using System.ComponentModel.DataAnnotations.Schema;
using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class Course : BaseEntity
{
    public Course()
    {
        Sections = new List<Section>();
        CourseNumbers = new List<CourseNumber>();
    }
    
    public string Title { get; set; } = string.Empty;
    public ICollection<CourseNumber> CourseNumbers { get; set; }
    public ICollection<Section> Sections { get; set; }

    public bool HasCourseNumber(string courseNumber)
    {
        var courseNum = CourseNumbers
            .FirstOrDefault(cn => cn.GetDepartmentAndNumber() == courseNumber);
        return courseNum != null;
    }
}