using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;

namespace Barbarosoft.TreeHouse.Service;

public class InstructorService : IInstructorService
{
    readonly IInstructorRepository _instructorRepository;
    public InstructorService(IInstructorRepository instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<InstructorEntity[]> GetAll()
    {
        return await _instructorRepository.ListAsync();
    }

    public async Task<InstructorEntity> GetById(int instructorId)
    {
        return await _instructorRepository.GetById(instructorId);
    }

    public async Task<CourseEntity[]> GetCoursesOfInstructor(int instructorId)
    {
        return await _instructorRepository.GetCoursesOfInstructor(instructorId);
    }
}