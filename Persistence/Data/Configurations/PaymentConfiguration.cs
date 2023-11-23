using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PaymentConfiguration:IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("payment");

        builder.HasIndex(e => e.PaymentMethodId, "paiment_method_id_idx");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.PaymentDate).HasColumnName("payment_date");
        builder.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
        builder.Property(e => e.Total)
            .HasPrecision(15, 2)
            .HasColumnName("total");

        builder.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
            .HasForeignKey(d => d.PaymentMethodId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("payment_method_id");        
    }

}