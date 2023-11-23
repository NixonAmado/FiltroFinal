using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CustomerConfiguration:IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("customer");

        builder.HasIndex(e => e.AddressId, "adress_id_idx");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AddressId).HasColumnName("address_id");
        builder.Property(e => e.ContactLastName)
            .HasMaxLength(30)
            .HasColumnName("contact_lastName");
        builder.Property(e => e.ContactName)
            .HasMaxLength(30)
            .HasColumnName("contact_name");
        builder.Property(e => e.CreditLimit)
            .HasPrecision(15, 2)
            .HasColumnName("credit_limit");
        builder.Property(e => e.Fax)
            .HasMaxLength(15)
            .HasColumnName("fax");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        builder.Property(e => e.Phone)
            .HasMaxLength(15)
            .HasColumnName("phone");

        builder.HasOne(d => d.Address).WithMany(p => p.Customers)
            .HasForeignKey(d => d.AddressId)
            .HasConstraintName("adress_id");        
    }

}