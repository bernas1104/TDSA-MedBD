using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDSAMedBDDomain.Models;

namespace TDSAMedBDDomain.Mappings {
  public class SpecialtyMapping : IEntityTypeConfiguration<Specialty> {
    public void Configure(EntityTypeBuilder<Specialty> builder) {
      builder.HasKey(x => x.Id);
      builder.HasAlternateKey(x => x.Name);

      builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
      builder.Property(x => x.CreatedAt)
        .HasColumnName("created_at")
        .ValueGeneratedOnAdd();
      builder.Property(x => x.UpdatedAt)
        .HasColumnName("updated_at")
        .ValueGeneratedOnAddOrUpdate();
      builder.Property(x => x.DeletedAt)
        .HasColumnName("deleted_at")
        .IsRequired(false);
    }
  }
}
