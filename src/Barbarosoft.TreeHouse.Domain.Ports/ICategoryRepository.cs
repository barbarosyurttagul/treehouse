using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Domain.Ports;

public interface ICategoryRepository
{
    Task<CategoryEntity[]> GetAll();
    Task<CategoryEntity> GetById(int categoryId);
}
