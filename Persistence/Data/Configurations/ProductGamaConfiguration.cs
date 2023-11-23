using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ProductGamaConfiguration:IEntityTypeConfiguration<ProductGama>
{
    public void Configure(EntityTypeBuilder<ProductGama> builder)
    {
        builder.HasKey(e => e.Gama).HasName("PRIMARY");

        builder.ToTable("product_gama");

        builder.Property(e => e.Gama)
            .HasMaxLength(50)
            .HasColumnName("gama");
        builder.Property(e => e.HtmlDescription)
            .HasColumnType("mediumtext")
            .HasColumnName("html_description");
        builder.Property(e => e.Image)
            .HasMaxLength(256)
            .HasColumnName("image");
        builder.Property(e => e.TextDescription)
            .HasColumnType("mediumtext")
            .HasColumnName("text_description");        
    }

}