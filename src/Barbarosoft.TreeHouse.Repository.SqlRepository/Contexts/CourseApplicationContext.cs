using Barbarosoft.TreeHouse.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;

public class CourseApplicationContext : DbContext, ICourseApplicationContext
{
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<CourseEntity> Courses { get; set; } = null!;
    public DbSet<InstructorEntity> Instructors { get; set; } = null!;
}