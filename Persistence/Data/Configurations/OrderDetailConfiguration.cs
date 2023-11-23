using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class OrderDetailConfiguration:IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.ProductId })
            .HasName("PRIMARY")
            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        builder.ToTable("order_detail");

        builder.HasIndex(e => e.OrderId, "order_id_idx");

        builder.HasIndex(e => e.ProductId, "product_id_idx");

        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder.Property(e => e.ProductId)
            .HasMaxLength(15)
            .HasColumnName("product_id");
        builder.Property(e => e.Cantidad)
            .HasMaxLength(45)
            .HasColumnName("cantidad");
        builder.Property(e => e.LineNumber).HasColumnName("line_number");
        builder.Property(e => e.UnitPrice)
            .HasPrecision(15, 2)
            .HasColumnName("unit_price");

        builder.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("order_id");

        builder.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("product_id");        
    }

}