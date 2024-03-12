using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

public class InstructorRepository : IInstructorRepository
{
    readonly ICourseApplicationContext _courseApplicationContext;
    public InstructorRepository(ICourseApplicationContext courseApplicationContext)
    {
        _courseApplicationContext = courseApplicationContext;
    }

    public async Task Create(InstructorEntity instructor)
    {
        await _courseApplicationContext.Instructors.AddAsync(instructor);
        _courseApplicationContext.SaveChanges();
    }

    public async Task<InstructorEntity> GetById(int instructorId)
    {
        var instructor = await _courseApplicationContext.Instructors
            .FirstOrDefaultAsync(x => x.InstructorId == instructorId);
        if (instructor != null)
            return instructor;
        throw new ArgumentNullException(nameof(instructorId));
    }

    public async Task<CourseEntity[]> GetCoursesOfInstructor(int instructorId)
    {
        var courses = await (from instructor in _courseApplicationContext.Instructors
                             join course in _courseApplicationContext.Courses
                             on instructor.CourseId equals course.CourseId
                             where instructor.InstructorId == instructorId
                             select course)
                      .ToArrayAsync();
        return courses;
    }

    public async Task<InstructorEntity[]> ListAsync()
    {
        return await _courseApplicationContext.Instructors.ToArrayAsync();
    }
}
