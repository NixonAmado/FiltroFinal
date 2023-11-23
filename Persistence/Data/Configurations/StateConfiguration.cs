using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class StateConfiguration:IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("state");

        builder.HasIndex(e => e.CountryId, "IX_state_CountryId");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(45)
            .HasColumnName("name");

        builder.HasOne(d => d.Country).WithMany(p => p.States)
            .HasForeignKey(d => d.CountryId)
            .HasConstraintName("city_id");
    }

}