using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("departments_positions");

        builder.HasKey(dp => new { dp.DepartmentId, dp.PositionId })
            .HasName("pk_department_position");

        builder.Property(dp => dp.DepartmentId)
            .IsRequired()
            .HasColumnName("department_id");

        builder.Property(dp => dp.PositionId)
            .IsRequired()
            .HasColumnName("position_id");
    }
}