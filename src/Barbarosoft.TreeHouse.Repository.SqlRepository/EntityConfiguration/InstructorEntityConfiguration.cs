using Barbarosoft.TreeHouse.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.EntityConfiguration;

public sealed class InstructorEntityConfiguration : IEntityTypeConfiguration<InstructorEntity>
{
    public void Configure(EntityTypeBuilder<InstructorEntity> builder)
    {
        builder.ToTable(RepositoryHelper.GetPluralTableNameFromEntity<InstructorEntity>());
        builder.HasKey(x => x.InstructorId);
    }
}