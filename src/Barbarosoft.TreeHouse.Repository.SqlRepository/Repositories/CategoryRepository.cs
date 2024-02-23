using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

public class CategoryRepository : ICategoryRepository
{
    ICourseApplicationContext _context;

    public CategoryRepository(ICourseApplicationContext context)
    {
        _context = context;
    }
    public async Task<CategoryEntity[]> GetAll()
    {
        return await _context.Categories.ToArrayAsync();
    }

    public async Task<CategoryEntity> GetById(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        if (category == null)
            throw new NullReferenceException();
        return category;
    }
}
