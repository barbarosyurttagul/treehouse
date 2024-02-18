using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Domain.Ports;

public interface ICourseRepository
{
    IEnumerable<CourseEntity> GetAll();
    Task<CourseEntity[]> GetByCategoryId(int categoryId);
}
