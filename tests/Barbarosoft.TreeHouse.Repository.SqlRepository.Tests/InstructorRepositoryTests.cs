using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
public class InstructorRepositoryTests
{
    ICourseApplicationContext _courseApplicationContext;
    InstructorRepository _instructorRepository;

    [SetUp]
    public void BeforeEach()
    {
        DbContextHelper.ResetDatabase();
        _courseApplicationContext = DbContextHelper.Context;
        _instructorRepository = new InstructorRepository(_courseApplicationContext);
    }
    [Test]
    public async Task ReturnsAllInstructors()
    {
        // Arrange
        var instructorId = 1;
        var firstName = "TestFirst";
        var lastName = "TestLast";
        AddInstructorEntity(instructorId, firstName, lastName);

        // Act
        var instructors = await _instructorRepository.ListAsync();

        // Assert
        Assert.That(instructors, Has.Length.EqualTo(1));
    }

    [Test]
    public async Task ReturnsInstructorWithInstructorId()
    {
        // Arrange
        int categoryId = 1;
        int courseId = 1;
        int instructorId = 1;
        string courseName = "Full Stack Web Developer";
        var categoryName = "Programming";
        var firstName = "TestFirst";
        var lastName = "TestLast";
        AddCategoryEntity(categoryId, categoryName: categoryName);
        AddCourseEntity(courseId, courseName, categoryId);
        AddInstructorEntity(instructorId, firstName, lastName, courseId);

        // Act
        var instructor = await _instructorRepository.GetById(instructorId);

        // Assert
        Assert.That(instructor.FirstName, Is.EqualTo("TestFirst"));
    }

    [Test]
    public void ThrowsArgumentNullExceptionWhenInstructorHasNoCourse()
    {
        // Arrange
        int instructorId = -1;

        // Act-Assert
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _instructorRepository.GetById(instructorId);
        });
        Assert.That(exception.ParamName, Is.EqualTo(nameof(instructorId)));
    }

    [Test]
    public async Task ReturnsCoursesOfInstructor()
    {
        // Arrange
        var instructorId = 1;
        var categoryId = 1;
        var courseId = 1;
        var categoryName = "Web";
        var courseName = "Programming";
        var firstName = "TestFirstName";
        var lastName = "TestLastName";
        AddCategoryEntity(categoryId, categoryName);
        AddCourseEntity(courseId, courseName, categoryId);
        AddInstructorEntity(instructorId, firstName, lastName, courseId);

        // Act
        var courses = await _instructorRepository.GetCoursesOfInstructor(instructorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Is.TypeOf<CourseEntity[]>());
            Assert.That(courses, Has.Length.EqualTo(1));
        });
    }

    [Test]
    public async Task ReturnsNoCourseForInstructorIfInstructorHasNoCourse()
    {
        // Arrange
        var instructorId = 1;
        var categoryId = 1;
        var courseId = 1;
        var categoryName = "Web";
        var courseName = "Programming";
        var firstName = "TestFirstName";
        var lastName = "TestLastName";
        AddCategoryEntity(categoryId, categoryName);
        AddCourseEntity(courseId, courseName, categoryId);
        AddInstructorEntity(instructorId, firstName, lastName);

        // Act
        var courses = await _instructorRepository.GetCoursesOfInstructor(instructorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Is.TypeOf<CourseEntity[]>());
            Assert.That(courses, Has.Length.EqualTo(0));
        });
    }

    private void AddCategoryEntity(int categoryId, string categoryName)
    {
        _courseApplicationContext.Categories.Add(
            new CategoryEntity
            {
                CategoryId = categoryId,
                Name = categoryName
            }
        );
        DbContextHelper.Context.SaveChanges();
    }

    private void AddCourseEntity(int courseId, string courseName, int categoryId)
    {
        _courseApplicationContext.Courses.Add(
            new CourseEntity
            {
                CourseId = courseId,
                Name = courseName,
                CategoryId = categoryId
            }
        );
        DbContextHelper.Context.SaveChanges();
    }

    private void AddInstructorEntity(int instructorId, string firstName, string lastName, int? courseId = null)
    {
        _courseApplicationContext.Instructors.Add(
            new InstructorEntity
            {
                InstructorId = instructorId,
                FirstName = firstName,
                LastName = lastName,
                CourseId = courseId
            }
        );
        DbContextHelper.Context.SaveChanges();
    }
}