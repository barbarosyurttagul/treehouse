using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

public class CourseRepository : ICourseRepository
{
    ICourseApplicationContext _courseApplicationContext;

    public CourseRepository(ICourseApplicationContext courseApplicationContext)
    {
        _courseApplicationContext = courseApplicationContext;
    }

    public IEnumerable<CourseEntity> GetAll()
    {
        return _courseApplicationContext.Courses.ToList();
    }

    public async Task<CourseEntity[]> GetByCategoryId(int categoryId)
    {
        return await _courseApplicationContext.Courses
            .Where(x => x.CategoryId == categoryId)
            .ToArrayAsync();
    }
}
