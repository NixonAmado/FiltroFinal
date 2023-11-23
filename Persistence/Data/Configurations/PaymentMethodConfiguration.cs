using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PaymentMethodConfiguration:IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("payment_method");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Description)
            .HasMaxLength(50)
            .HasColumnName("description");        
    }

}