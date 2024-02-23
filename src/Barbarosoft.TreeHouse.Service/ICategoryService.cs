using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Service;

public interface ICategoryService
{
    Task<CategoryEntity[]> GetAll();
    Task<CategoryEntity> GetById(int categoryId);
}