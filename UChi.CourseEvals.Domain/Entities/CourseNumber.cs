using UChi.CourseEvals.Domain.Common;

namespace UChi.CourseEvals.Domain.Entities;

public class CourseNumber : BaseEntity
{
    public CourseNumber()
    { }

    public CourseNumber(string deptAndNumString, int courseId)
    {
        if (deptAndNumString == null)
        {
            throw new Exception("Department is null");
        }

        var splitDeptAndNum = deptAndNumString.Split(" ");
        if (splitDeptAndNum.Length != 2)
        {
            throw new Exception("Invalid string and department format");
        }
        Department = splitDeptAndNum[0];
        Number = int.Parse(splitDeptAndNum[1]);
        CourseId = courseId;
        return;
    }
    
    public int CourseId { get; set; }
    public string Department { get; set; } = string.Empty;
    public int Number { get; set; }

    public string GetDepartmentAndNumber()
    {
        return string.Concat(Department, " ", Number);
    }
}