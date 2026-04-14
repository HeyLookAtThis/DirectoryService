using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");
        
        builder.HasKey(p => p.Id)
            .HasName("pk_positions");
        
        builder.ComplexProperty(p => p.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name");
        });
        
        builder.ComplexProperty(p => p.Description, nb =>
        {
            nb.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("description");
        });
        
        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasColumnName("is_active");
        
        builder.Property(p => p.CreateAt)
            .IsRequired()
            .HasColumnName("create_at");

        builder.Property(p => p.UpdateAt)
            .IsRequired()
            .HasColumnName("update_at");
    }
}