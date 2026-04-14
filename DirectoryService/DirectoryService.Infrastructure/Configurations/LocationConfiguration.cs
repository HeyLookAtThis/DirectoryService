using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class LocationConfiguration: IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");
        
        builder.Property(l => l.Id)
            .IsRequired()
            .HasColumnName("id");
        
        builder.HasKey(l => l.Id)
            .HasName("pk_locations");

        builder.ComplexProperty(l => l.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name");
        });

        builder.ComplexProperty(l => l.Address, nb =>
        {
            nb.ToJson("address");

            nb.Property(a => a.PostalСode)
                .IsRequired();

            nb.Property(a => a.Country)
                .IsRequired();

            nb.Property(a => a.Region)
                .IsRequired();

            nb.Property(a => a.City)
                .IsRequired();

            nb.Property(a => a.Street)
                .IsRequired();

            nb.Property(a => a.House)
                .IsRequired();
        });

        builder.ComplexProperty(l => l.Timezone, nb =>
        {
            nb.Property(tz => tz.Value)
                .IsRequired()
                .HasColumnName("time_zone");
        });
        
        builder.Property(l => l.IsActive)
            .IsRequired()
            .HasColumnName("is_active");
        
        builder.Property(l => l.CreateAt)
            .IsRequired()
            .HasColumnName("create_at");

        builder.Property(l => l.UpdateAt)
            .IsRequired()
            .HasColumnName("update_at");
    }
}