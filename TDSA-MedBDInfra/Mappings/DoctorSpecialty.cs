using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDInfra.Mappings {
  public class DoctorSpecialtyMapping : IEntityTypeConfiguration<DoctorSpecialty> {
    public void Configure(EntityTypeBuilder<DoctorSpecialty> builder) {
      builder.HasKey(x => new { x.DoctorId, x.SpecialtyId });

      builder.HasOne(x => x.Doctor)
        .WithMany(x => x.Specialties)
        .HasForeignKey(x => x.DoctorId);
      builder.HasOne(x => x.Specialty)
        .WithMany(x => x.Doctors)
        .HasForeignKey(x => x.SpecialtyId);
    }
  }
}
