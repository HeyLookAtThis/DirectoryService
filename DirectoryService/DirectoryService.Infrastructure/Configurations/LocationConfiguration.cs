using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class LocationConfiguration: IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");
        
        builder.HasKey(l => l.Id).HasName("pk_locations");

        builder.ComplexProperty(l => l.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name");
        });

        builder.ComplexProperty(l => l.Address, nb =>
        {
            nb.Property(a => a.PostalСode)
                .IsRequired()
                .HasColumnName("postal_code");

            nb.Property(a => a.Country)
                .IsRequired()
                .HasColumnName("country");

            nb.Property(a => a.Region)
                .IsRequired()
                .HasColumnName("region");

            nb.Property(a => a.City)
                .IsRequired()
                .HasColumnName("city");

            nb.Property(a => a.Street)
                .IsRequired()
                .HasColumnName("street");

            nb.Property(a => a.House)
                .IsRequired()
                .HasColumnName("house");
        });

        builder.ComplexProperty(l => l.Timezone, nb =>
        {
            nb.Property(tz => tz.Value)
                .IsRequired()
                .HasColumnName("time_zone");
        });
        
        builder.Property(l => l.IsActive).IsRequired().HasColumnName("is_active");
        
        builder.Property(l => l.CreateAt).IsRequired().HasColumnName("create_at");

        builder.Property(l => l.UpdateAt).IsRequired().HasColumnName("update_at");
        
        builder.OwnsMany(l => l.Departments, nb =>
        {
            nb.ToJson("departments");

            nb.Property(dl => dl.DepartmentId).IsRequired();
            nb.Property(dl => dl.LocationId).IsRequired();
        });
    }
}