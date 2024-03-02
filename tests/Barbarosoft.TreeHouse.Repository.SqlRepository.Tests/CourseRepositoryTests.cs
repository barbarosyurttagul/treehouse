using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
internal class CourseRepositoryTests
{
    ICourseApplicationContext _courseApplicationContext;
    CourseRepository _courseRepository;

    [SetUp]
    public void BeforeEach()
    {
        DbContextHelper.ResetDatabase();
        _courseApplicationContext = DbContextHelper.Context;
        _courseRepository = new CourseRepository(_courseApplicationContext);
    }

    [Test]
    public async Task ReturnsAllCourses()
    {
        // Arrange
        DbContextHelper.Add(new CourseEntity
        {
            CourseId = 1,
            Name = "Full Stack Web Developer"
        });
        DbContextHelper.Add(new CourseEntity
        {
            CourseId = 2,
            Name = "Sql Developer"
        });

        // Act
        var courses = await _courseRepository.GetAll();

        // Assert
        Assert.That(courses, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task ReturnsAllCoursesForGivenCategoryId()
    {
        // Arrange
        int categoryId = 1;
        string courseName = "Full Stack Web Developer";
        DbContextHelper.Add(new CategoryEntity
        {
            CategoryId = categoryId,
            Name = "Programming"
        });
        DbContextHelper.Add(new CourseEntity
        {
            CourseId = 1,
            Name = courseName,
            CategoryId = categoryId
        });

        // Act
        var courses = await _courseRepository.GetByCategoryId(categoryId);

        // Assert
        Assert.That(courses[0].Name, Is.EqualTo(courseName));
    }
}