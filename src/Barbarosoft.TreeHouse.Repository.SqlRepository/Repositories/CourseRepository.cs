using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

public class CourseRepository : ICourseRepository
{
    readonly ICourseApplicationContext _context;

    public CourseRepository(ICourseApplicationContext context)
    {
        _context = context;
    }

    public async Task Create(CourseEntity courseEntity)
    {
        await _context.Courses.AddAsync(courseEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<CourseEntity[]> GetAll()
    {
        return await _context.Courses.ToArrayAsync();
    }

    public async Task<CourseEntity[]> GetByCategoryId(int categoryId)
    {
        return await _context.Courses
            .Where(x => x.CategoryId == categoryId)
            .ToArrayAsync();
    }
}
