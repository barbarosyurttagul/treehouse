using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Domain.Ports;

public interface IInstructorRepository
{
    Task<InstructorEntity[]> ListAsync();
    Task<InstructorEntity> GetById(int instructorId);
}
