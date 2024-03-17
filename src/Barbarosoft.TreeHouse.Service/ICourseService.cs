using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Service;

public interface ICourseService
{
    Task<CourseEntity[]> GetAll();
    Task<CourseEntity[]> GetByCategoryId(int categoryId);
    Task Create(CourseEntity courseEntity);
}