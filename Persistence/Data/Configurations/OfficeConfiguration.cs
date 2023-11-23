using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class OfficeConfiguration:IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("office");

        builder.HasIndex(e => e.AddressId, "addres_id_idx");

        builder.Property(e => e.Id)
            .HasMaxLength(10)
            .HasColumnName("id");
        builder.Property(e => e.AddressId).HasColumnName("address_id");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("phone");

        builder.HasOne(d => d.Address).WithMany(p => p.Offices)
            .HasForeignKey(d => d.AddressId)
            .HasConstraintName("address_id");        
    }

}