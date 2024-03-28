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

    public async Task<ServiceResult> Create(CourseEntity courseEntity)
    {
        var courseNameIsNull = IsCourseNameNull(courseEntity.Name);
        if (courseNameIsNull)
        {
            return new ServiceResult("Course name can not be null");
        }
        var categoryIdInvalid = IsCategoryIdNull(courseEntity.CategoryId);
        if (categoryIdInvalid)
        {
            return new ServiceResult("Category Id can not be equal or less than 0");
        }
        await _courseRepository.Create(courseEntity);
        return new ServiceResult();
    }

    public Task<CourseEntity[]> GetAll()
    {
        return _courseRepository.GetAll();
    }

    public Task<CourseEntity[]> GetByCategoryId(int categoryId)
    {
        return _courseRepository.GetByCategoryId(categoryId);
    }

    private static bool IsCourseNameNull(string courseName)
    {
        if (courseName is null)
        {
            return true;
        }
        return false;
    }
    private static bool IsCategoryIdNull(int categoryId)
    {
        if (categoryId <= 0)
        {
            return true;
        }
        return false;
    }
}
