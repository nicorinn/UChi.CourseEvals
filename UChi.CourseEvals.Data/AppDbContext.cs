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
            .UseNpgsql()
            .UseSnakeCaseNamingConvention();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "Honors Introduction to Computer Science II",
                AverageSentiment = 0.7,
                ChartData = "{}"
            });
        
        modelBuilder.Entity<CourseNumber>().HasData(
            new CourseNumber
            {
                Id = 1,
                CourseId = 1,
                DepartmentAndNumber = "CMSC 16200"
            },
            new CourseNumber
            {
                Id = 2,
                CourseId = 1,
                DepartmentAndNumber = "TEST 16200"
            });

        modelBuilder.Entity<Section>().HasData(
            new Section
            {
                Id = 1,
                CourseId = 1,
                Number = 1,
                Year = 2022,
                Quarter = Quarter.Winter,
                Sentiment = 0.8,
                ChartData = "{}",
            },
            new Section
            {
                Id = 2,
                CourseId = 1,
                Number = 2,
                Year = 2022,
                Quarter = Quarter.Winter,
                Sentiment = 0.8,
                ChartData = "{}",
            });

        modelBuilder.Entity<Professor>().HasData(
            new Professor {Id = 1, Name = "Fred Chong"}, new Professor {Id = 2, Name = "Hank Hoffmann"});

        modelBuilder.Entity<ApiKey>().HasData(new ApiKey
        {
            Id = 1,
            Key = "43ae2c82-dbf7-4e74-a5dc-d9d45783cc6e",
            Email = "test@test.com",
            ExpirationDate = new DateTime(2022, 05, 01),
            CreationDate = DateTime.Now,
            LastUsed = null,
            RequestCount = 0
        });
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<CourseNumber> CourseNumbers { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
}