using Microsoft.EntityFrameworkCore;
using TDSAMedBDDomain.Mappings;
using TDSAMedBDDomain.Models;

namespace TDSAMedBDDomain.Context {
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
    }
  }
}
