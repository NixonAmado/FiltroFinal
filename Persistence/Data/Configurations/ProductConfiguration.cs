using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("product");

        builder.HasIndex(e => e.Gama, "gama_idx");

        builder.Property(e => e.Id)
            .HasMaxLength(15)
            .HasColumnName("id");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.Dimentions)
            .HasMaxLength(45)
            .HasColumnName("dimentions");
        builder.Property(e => e.Gama)
            .HasMaxLength(50)
            .HasColumnName("gama");
        builder.Property(e => e.Name)
            .HasMaxLength(70)
            .HasColumnName("name");
        builder.Property(e => e.SalePrice)
            .HasPrecision(15, 2)
            .HasColumnName("sale_price");
        builder.Property(e => e.Stock).HasColumnName("stock");
        builder.Property(e => e.Supplier)
            .HasMaxLength(45)
            .HasColumnName("supplier");
        builder.Property(e => e.SupplierPrice)
            .HasPrecision(15, 2)
            .HasColumnName("supplier_price");

        builder.HasOne(d => d.GamaNavigation).WithMany(p => p.Products)
            .HasForeignKey(d => d.Gama)
            .HasConstraintName("gama");        
    }

}