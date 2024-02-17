using System.Reflection;
using Barbarosoft.TreeHouse.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;

public class CourseApplicationContext : DbContext, ICourseApplicationContext
{
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<CourseEntity> Courses { get; set; } = null!;
    public DbSet<InstructorEntity> Instructors { get; set; } = null!;

    public CourseApplicationContext(DbContextOptions<CourseApplicationContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(SqlConstants.CourseAppSchema);
    }
}