using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("employee");

            builder.HasIndex(e => e.BossId, "boss_id_idx");

            builder.HasIndex(e => e.OfficeId, "office_id_idx");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.BossId).HasColumnName("boss_id");
            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            builder.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            builder.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("job_title");
            builder.Property(e => e.LastName1)
                .HasMaxLength(50)
                .HasColumnName("lastName1");
            builder.Property(e => e.LastName2)
                .HasMaxLength(50)
                .HasColumnName("lastName2");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            builder.Property(e => e.OfficeId)
                .HasMaxLength(10)
                .HasColumnName("office_id");

            builder.HasOne(d => d.Boss).WithMany(p => p.InverseBoss)
                .HasForeignKey(d => d.BossId)
                .HasConstraintName("boss_id");

            builder.HasOne(d => d.Office).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("office_id");        
    }

}