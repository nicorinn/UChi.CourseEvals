using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql("Host=localhost;Database=uchi_evals;Username=uchi_evals_server;Include Error Detail=true")
            .UseSnakeCaseNamingConvention();

    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<CourseNumber> CourseNumbers { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
}