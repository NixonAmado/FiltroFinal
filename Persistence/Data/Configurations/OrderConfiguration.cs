using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("order");

        builder.HasIndex(e => e.CustomerId, "custumer_id_idx");

        builder.HasIndex(e => e.EmployeeId, "employee_id_idx");

        builder.HasIndex(e => e.PaymentId, "payment_id_idx");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.Comments).HasColumnName("comments");
        builder.Property(e => e.CustomerId).HasColumnName("customer_id");
        builder.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
        builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
        builder.Property(e => e.ExpectedDate).HasColumnName("expected_date");
        builder.Property(e => e.OrderDate).HasColumnName("order_date");
        builder.Property(e => e.PaymentId).HasColumnName("payment_id");
        builder.Property(e => e.Status)
            .HasMaxLength(20)
            .HasColumnName("status");

        builder.HasOne(d => d.Customer).WithMany(p => p.Orders)
            .HasForeignKey(d => d.CustomerId)
            .HasConstraintName("custumer_id");

        builder.HasOne(d => d.Employee).WithMany(p => p.Orders)
            .HasForeignKey(d => d.EmployeeId)
            .HasConstraintName("employee_id");

        builder.HasOne(d => d.Payment).WithMany(p => p.Orders)
            .HasForeignKey(d => d.PaymentId)
            .HasConstraintName("payment_id");

    }

}