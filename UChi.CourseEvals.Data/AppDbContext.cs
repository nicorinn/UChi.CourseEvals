using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSnakeCaseNamingConvention();

    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<CourseNumber> CourseNumbers { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
}