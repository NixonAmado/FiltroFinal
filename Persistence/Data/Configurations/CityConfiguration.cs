using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CityConfiguration:IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("city");

        builder.HasIndex(e => e.StateId, "IX_city_StateId");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(45)
            .HasColumnName("name");

        builder.HasOne(d => d.State).WithMany(p => p.Cities).HasForeignKey(d => d.StateId);
            
    }

}