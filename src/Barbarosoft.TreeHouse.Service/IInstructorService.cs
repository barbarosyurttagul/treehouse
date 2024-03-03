using Barbarosoft.TreeHouse.Domain.Model;

namespace Barbarosoft.TreeHouse.Service;

public interface IInstructorService
{
    Task<InstructorEntity[]> GetAll();
    Task<InstructorEntity> GetById(int instructorId);
}