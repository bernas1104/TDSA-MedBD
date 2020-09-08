using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDInfra.Mappings {
  public class DoctorMapping : IEntityTypeConfiguration<Doctor> {
    public void Configure(EntityTypeBuilder<Doctor> builder) {
      builder.HasKey(x => x.Id);
      builder.HasAlternateKey(x => x.Cpf);
      builder.HasAlternateKey(x => x.Crm);

      builder.Property(x => x.Id).HasColumnType("uuid").UseIdentityColumn();
      builder.Property(x => x.Fullname).HasMaxLength(255).IsRequired();
      builder.Property(x => x.Cpf).HasMaxLength(11).IsRequired();
      builder.Property(x => x.Crm).HasMaxLength(50).IsRequired();
      builder.Property(x => x.CreatedAt)
        .HasColumnName("created_at")
        .HasDefaultValueSql("now()")
        .ValueGeneratedOnAdd();
      builder.Property(x => x.UpdatedAt)
        .HasColumnName("updated_at")
        .HasDefaultValueSql("now()")
        .ValueGeneratedOnAddOrUpdate();
      builder.Property(x => x.DeletedAt)
        .HasColumnName("deleted_at")
        .IsRequired(false);
    }
  }
}
