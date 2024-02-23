using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
internal class CategoryRepositoryTests
{
    ICourseApplicationContext _courseApplicationContext;
    ICategoryRepository _categoryRepository;

    [SetUp]
    public void BeforeEach()
    {
        DbContextHelper.ResetDatabase();
        _courseApplicationContext = DbContextHelper.Context;
        _categoryRepository = new CategoryRepository(_courseApplicationContext);
    }

    [Test]
    public async Task ReturnsAllCategories()
    {
        // Arrange
        DbContextHelper.Add(new CategoryEntity
        {
            CategoryId = 1,
            Name = "programming"
        });
        DbContextHelper.Add(new CategoryEntity
        {
            CategoryId = 2,
            Name = "English"
        });

        // Act
        var categories = await _categoryRepository.GetAll();

        // Assert
        Assert.That(categories.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task ReturnsCategoryById()
    {
        // Arrange
        int id = 3;
        var categoryName = "programming";
        DbContextHelper.Add(new CategoryEntity
        {
            CategoryId = id,
            Name = categoryName
        });

        // Act
        var category = await _categoryRepository.GetById(id);

        // Assert
        Assert.That(category.Name, Is.EqualTo(categoryName));
    }

    [Test]
    public void ThrowsNullReferenceExceptionWhenCategoryIdDoesNotExist()
    {
        var categoryId = -1;
        // Act
        Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            await _categoryRepository.GetById(categoryId);
        });
    }
}