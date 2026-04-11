using DirectoryService.Domain.Departments;
using DirectoryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        
        builder.Property(d => d.Id)
            .IsRequired()
            .HasColumnName("id");
        
        builder.HasKey(d => d.Id)
            .HasName("pk_departments");
        
        builder.ComplexProperty(d => d.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name");
        });

        builder.ComplexProperty(d => d.Identifier, nb =>
        {
            nb.Property(i => i.Value)
                .IsRequired()
                .HasColumnName("identifier");
        });

        builder.Property(d => d.ParentId)
            .IsRequired(false)
            .HasColumnName("parent_id");

        builder.ComplexProperty(d => d.Path, nb =>
        {
            nb.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("path");
        });

        builder.ComplexProperty(d => d.Depth, nb =>
        {
            nb.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("depth");
        });

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(d => d.CreateAt)
            .IsRequired()
            .HasColumnName("create_at");

        builder.Property(d => d.UpdateAt)
            .IsRequired()
            .HasColumnName("update_at");
    }
}