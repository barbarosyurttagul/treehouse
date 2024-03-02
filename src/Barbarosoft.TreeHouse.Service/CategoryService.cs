using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;

namespace Barbarosoft.TreeHouse.Service;

public class CategoryService : ICategoryService
{
    readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<CategoryEntity[]> GetAll()
    {
        return await _categoryRepository.GetAll();
    }

    public async Task<CategoryEntity> GetById(int categoryId)
    {
        return await _categoryRepository.GetById(categoryId);
    }
}