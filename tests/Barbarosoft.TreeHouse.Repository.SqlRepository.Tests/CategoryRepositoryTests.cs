using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;
using NSubstitute;
using System.Reflection;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
public class CategoryRepositoryTests
{
    ICourseApplicationContext _courseApplicationContext;
    CategoryRepository _categoryRepository;

    [SetUp]
    public void BeforeEach()
    {
        DbContextHelper.ResetDatabase();
        _courseApplicationContext = DbContextHelper.Context;
        _categoryRepository = new CategoryRepository(_courseApplicationContext);
    }

    [Test]
    public void ConstructorInitializesContext()
    {
        // Arrange
        var context = Substitute.For<ICourseApplicationContext>(); // Use a mock or a test double

        // Act
        var repository = new CategoryRepository(context);

        // Assert
        var contextField = typeof(CategoryRepository).GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var actualContext = contextField!.GetValue(repository);

        Assert.That(actualContext, Is.EqualTo(context));
        Assert.That(_categoryRepository, Is.Not.Null);
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
        Assert.Multiple(() =>
        {
            Assert.That(categories, Has.Length.EqualTo(2));
            Assert.That(categories[0].CategoryId, Is.EqualTo(1));
            Assert.That(categories[1].CategoryId, Is.EqualTo(2));
        });
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
    public void ThrowsArgumentNullExceptionWhenCategoryIdDoesNotExist()
    {
        var categoryId = -1;
        // Act
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _categoryRepository.GetById(categoryId);
        });
        Assert.That(exception.ParamName, Is.EqualTo(nameof(categoryId)));
    }
}