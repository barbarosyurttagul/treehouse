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

    public async Task<CourseEntity[]> GetAll()
    {
        return await _courseApplicationContext.Courses.ToArrayAsync();
    }

    public async Task<CourseEntity[]> GetByCategoryId(int categoryId)
    {
        return await _courseApplicationContext.Courses
            .Where(x => x.CategoryId == categoryId)
            .ToArrayAsync();
    }
}
