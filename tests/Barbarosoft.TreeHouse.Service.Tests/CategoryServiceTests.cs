using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests;

[TestFixture(Category = "unit")]
public class CategoryServiceTests
{
    private ICategoryRepository _categoryRepositorySub;
    private ICategoryService _categoryServiceSub;

    public CategoryServiceTests()
    {
        _categoryRepositorySub = Substitute.For<ICategoryRepository>();
        _categoryServiceSub = new CategoryService(_categoryRepositorySub);
    }

    [Test]
    public async Task CallsExpectedMethodsWhenGettingAllCategories()
    {

        // Act
        await _categoryServiceSub.GetAll();

        // Assert
        await _categoryRepositorySub.Received(1).GetAll();
    }

    [Test]
    public async Task ReturnsAllCategories()
    {
        // Arrange
        int categoryId = 1;
        string categoryName = "Programming";
        _categoryRepositorySub.GetAll().Returns(new CategoryEntity[]{
            new CategoryEntity
            {
                CategoryId = categoryId, Name = categoryName
            }
        });

        // Act
        var categories = await _categoryServiceSub.GetAll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(categories[0].Name, Is.EqualTo(categoryName));
            Assert.That(categories[0].CategoryId, Is.EqualTo(categoryId));
            Assert.That(categories.Length, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task ReturnsOneCategoryWhenGetByIdIsCalled()
    {
        // Arrange
        int categoryId_1 = 1;
        string categoryName_1 = "Programming";
        _categoryRepositorySub.GetById(categoryId_1).Returns(new CategoryEntity
        {
            CategoryId = categoryId_1,
            Name = categoryName_1
        });

        // Act
        var category = await _categoryServiceSub.GetById(categoryId_1);

        // Assert
        await _categoryRepositorySub.Received(1).GetById(categoryId_1);
        Assert.That(category, Is.TypeOf<CategoryEntity>());
    }
}