using System.ComponentModel;

namespace UChi.CourseEvals.Domain.Enums;

public enum Quarter
{
    Autumn,
    Winter,
    Spring
}

public enum ApiKeyScope
{
    None,
    Read,
    Write,
    ReadAndWrite
}