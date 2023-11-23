using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class AddressConfiguration:IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
    builder.HasKey(e => e.Id).HasName("PRIMARY");

    builder.ToTable("address");
    builder.HasIndex(e => e.CityId, "City_id_idx");
    builder.Property(e => e.Id).HasColumnName("id");
    builder.Property(e => e.AddressLine2)
        .HasMaxLength(50)
        .HasColumnName("address_line2");
    builder.Property(e => e.AdressLine1)
        .HasMaxLength(50)
        .HasColumnName("adress_line1");
    builder.Property(e => e.PostalCode)
        .HasMaxLength(10)
        .HasColumnName("postal_code");
    builder.HasOne(d => d.City).WithMany(p => p.Addresses).HasForeignKey(d => d.CityId);        
    }

}