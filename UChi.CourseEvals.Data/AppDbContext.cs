using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        Courses = Set<Course>();
        Sections = Set<Section>();
        Professors = Set<Professor>();
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Courses = Set<Course>();
        Sections = Set<Section>();
        Professors = Set<Professor>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql("Host=localhost;Database=uchi_evals;Username=uchi_evals_server")
            .UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new {
                Id = 1,
                Title = "Honors Introduction to Computer Science II",
                Department = "CMSC",
                Number = 16200,
                AverageSentiment = 0.7,
                ChartData = "{}"
            });
        
        modelBuilder.Entity<Section>().HasData(
            new {
                Id = 1,
                CourseId = 1,
                Number = 1,
                Year = 2022,
                Quarter = Quarter.Winter,
                Sentiment = 0.8,
                ChartData = "{}",
                ProfessorId = 1
            },
            new
            {
                Id = 2,
                CourseId = 1,
                Number = 2,
                Year = 2022,
                Quarter = Quarter.Winter,
                Sentiment = 0.8,
                ChartData = "{}",
                ProfessorId = 2
            });
        
        modelBuilder.Entity<Professor>().HasData(
            new { Id = 1, Name = "Fred Chong" }, new { Id = 2, Name = "Hank Hoffmann" });
    }

public DbSet < Course > Courses {
    get;
    set;
}
public DbSet < Section > Sections {
    get;
    set;
}
public DbSet < Professor > Professors {
    get;
    set;
}

}