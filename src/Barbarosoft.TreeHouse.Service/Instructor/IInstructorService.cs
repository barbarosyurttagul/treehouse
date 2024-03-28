using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Service;

public interface IInstructorService
{
    Task<InstructorEntity[]> GetAll();
    Task<InstructorEntity> GetById(int instructorId);
    Task<CourseEntity[]> GetCoursesOfInstructor(int instructorId);
    Task Create(InstructorEntity instructor);
}