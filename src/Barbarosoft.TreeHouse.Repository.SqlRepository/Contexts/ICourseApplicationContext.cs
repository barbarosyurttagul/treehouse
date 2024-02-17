using Barbarosoft.TreeHouse.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
public interface ICourseApplicationContext
{
    DbSet<CategoryEntity> Categories { get; set; }
    DbSet<CourseEntity> Courses { get; set; }
    DbSet<InstructorEntity> Instructors { get; set; }
}