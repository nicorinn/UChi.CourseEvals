using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Data;

public interface IAppDbContext : IDisposable
{
    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default(CancellationToken));
    public EntityEntry Entry(object entity);
    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<CourseNumber> CourseNumbers { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }

}