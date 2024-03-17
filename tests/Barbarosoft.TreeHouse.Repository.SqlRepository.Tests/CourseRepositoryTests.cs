using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
public class CourseRepositoryTests
{
    ICourseApplicationContext _context;
    CourseRepository _courseRepository;

    [SetUp]
    public void BeforeEach()
    {
        DbContextHelper.ResetDatabase();
        _context = DbContextHelper.Context;
        _courseRepository = new CourseRepository(_context);
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

    [Test]
    public async Task ReturnsCorrectInfoWhenCreatingCourse()
    {
        // Arrange
        int courseId = 1;
        int categoryId = 1;
        string courseName = "Programming";
        var course = new CourseEntity
        {
            CourseId = courseId,
            Name = courseName,
            CategoryId = categoryId
        };

        // Act
        await _courseRepository.Create(course);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_context.Courses.Count(), Is.EqualTo(1));
            Assert.That(_context.Courses.First().Name, Is.EqualTo(courseName));
        });
    }
}