using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

public class CategoryRepository : ICategoryRepository
{
    ICourseApplicationContext _context;

    public CategoryRepository(ICourseApplicationContext context)
    {
        _context = context;
    }
    public IEnumerable<CategoryEntity> GetAll()
    {
        return _context.Categories.ToList();
    }

    public CategoryEntity GetById(int categoryId)
    {
        var category = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (category == null)
            throw new NullReferenceException();
        return category;
    }
}
