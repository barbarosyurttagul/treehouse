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
    public void ReturnsAllCategories()
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
        var categories = _categoryRepository.GetAll();

        // Assert
        Assert.That(categories.Count(), Is.EqualTo(2));
    }

    [Test]
    public void ReturnsCategoryById()
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
        var category = _categoryRepository.GetById(id);

        // Assert
        Assert.That(category.Name, Is.EqualTo(categoryName));
    }

    [Test]
    public void ThrowsNullReferenceExceptionWhenCategoryIdDoesNotExist()
    {
        var categoryId = -1;
        // Act
        Assert.Throws<NullReferenceException>(() =>
        {
            _categoryRepository.GetById(categoryId);
        });
    }
}