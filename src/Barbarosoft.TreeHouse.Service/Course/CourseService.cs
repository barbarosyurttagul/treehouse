using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Microsoft.Extensions.Localization;

namespace Barbarosoft.TreeHouse.Service;

public class CourseService : ICourseService
{
    readonly ICourseRepository _courseRepository;
    private readonly IStringLocalizer _localizer;
    public CourseService(ICourseRepository courseRepository, IStringLocalizer localizer)
    {
        _courseRepository = courseRepository;
        _localizer = localizer;
    }

    public async Task<ServiceResult> Create(CourseEntity courseEntity)
    {
        var validationResult = ValidateCourseEntity(courseEntity);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
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

    private ServiceResult ValidateCourseEntity(CourseEntity courseEntity)
    {
        var courseNameIsEmpty = IsCourseNameEmpty(courseEntity.Name);
        if (courseNameIsEmpty)
        {
            return new ServiceResult(_localizer["course_name_can_not_be_empty"]);
        }
        var categoryIdInvalid = IsCategoryIdInvalid(courseEntity.CategoryId);
        if (categoryIdInvalid)
        {
            return new ServiceResult(_localizer["category_id_invalid"]);
        }
        return new ServiceResult();
    }

    private static bool IsCourseNameEmpty(string courseName)
    {
        if (courseName == string.Empty)
        {
            return true;
        }
        return false;
    }
    private static bool IsCategoryIdInvalid(int categoryId)
    {
        if (categoryId <= 0)
        {
            return true;
        }
        return false;
    }
}
