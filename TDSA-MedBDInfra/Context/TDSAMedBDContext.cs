using Microsoft.EntityFrameworkCore;
using TDSA_MedBDInfra.Mappings;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDInfra.Context {
  public class TDSAMedBDContext : DbContext {
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }

    public TDSAMedBDContext(DbContextOptions<TDSAMedBDContext> options)
      : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder) {
      builder.ApplyConfiguration(new DoctorMapping());
      builder.ApplyConfiguration(new SpecialtyMapping());
      builder.ApplyConfiguration(new DoctorSpecialtyMapping());

      builder.Entity<Specialty>().HasData(
        new Specialty {
          Id = 1,
          Name = "Clínico Geral",
        }
      );
    }
  }
}
