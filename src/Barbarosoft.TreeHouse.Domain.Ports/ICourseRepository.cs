using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Domain.Ports;

public interface ICourseRepository
{
    Task<CourseEntity[]> GetAll();
    Task<CourseEntity[]> GetByCategoryId(int categoryId);
    Task Create(CourseEntity courseEntity);
}
