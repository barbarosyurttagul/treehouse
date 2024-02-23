using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
internal class InstructorRepositoryTests
{
    ICourseApplicationContext _courseApplicationContext;
    IInstructorRepository _instructorRepository;

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
        DbContextHelper.Add(new InstructorEntity
        {
            InstructorId = 1,
            FirstName = "TestFirst",
            LastName = "TestLast"
        });

        // Act
        var instructors = await _instructorRepository.ListAsync();

        // Assert
        Assert.That(instructors.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task ReturnsInstructorsWithCourseId()
    {
        // Arrange
        int categoryId = 1;
        int courseId = 1;
        int instructorId = 1;
        string courseName = "Full Stack Web Developer";
        DbContextHelper.Add(new CategoryEntity
        {
            CategoryId = categoryId,
            Name = "Programming"
        });
        DbContextHelper.Add(new CourseEntity
        {
            CourseId = courseId,
            Name = courseName,
            CategoryId = categoryId
        });
        DbContextHelper.Add(new InstructorEntity
        {
            InstructorId = instructorId,
            FirstName = "TestFirst",
            LastName = "TestLast",
            CourseId = courseId
        });

        // Act
        var instructor = await _instructorRepository.GetById(instructorId);

        // Assert
        Assert.That(instructor.FirstName, Is.EqualTo("TestFirst"));
    }

    [Test]
    public void ThrowsNullReferenceExceptionInstructorHasNoCourse()
    {
        // Arrange
        int instructorId = -1;

        // Act-Assert
        Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            await _instructorRepository.GetById(instructorId);
        });
    }
}