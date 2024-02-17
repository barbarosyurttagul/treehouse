using Barbarosoft.TreeHouse.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.EntityConfiguration;

public sealed class CourseEntityConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.ToTable(RepositoryHelper.GetPluralTableNameFromEntity<CourseEntity>());
        builder.HasKey(x => x.CourseId);
    }
}