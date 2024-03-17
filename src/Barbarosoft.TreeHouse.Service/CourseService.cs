using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;

namespace Barbarosoft.TreeHouse.Service;

public class CourseService : ICourseService
{
    readonly ICourseRepository _courseRepository;
    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task Create(CourseEntity courseEntity)
    {
        await _courseRepository.Create(courseEntity);
    }

    public Task<CourseEntity[]> GetAll()
    {
        return _courseRepository.GetAll();
    }

    public Task<CourseEntity[]> GetByCategoryId(int categoryId)
    {
        return _courseRepository.GetByCategoryId(categoryId);
    }
}