using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDInfra.Mappings {
  public class SpecialtyMapping : IEntityTypeConfiguration<Specialty> {
    public void Configure(EntityTypeBuilder<Specialty> builder) {
      builder.HasKey(x => x.Id);
      builder.HasAlternateKey(x => x.Name);

      builder.Property(x => x.Id).HasColumnType("uuid").UseIdentityColumn();
      builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
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
