using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;

public class InstructorRepository : IInstructorRepository
{
    ICourseApplicationContext _courseApplicationContext;
    public InstructorRepository(ICourseApplicationContext courseApplicationContext)
    {
        _courseApplicationContext = courseApplicationContext;
    }

    public async Task<InstructorEntity> GetById(int instructorId)
    {
        var instructor = await _courseApplicationContext.Instructors
            .FirstOrDefaultAsync(x => x.InstructorId == instructorId);
        if (instructor != null)
            return instructor;
        throw new NullReferenceException();
    }

    public async Task<InstructorEntity[]> ListAsync()
    {
        return await _courseApplicationContext.Instructors.ToArrayAsync();
    }
}
