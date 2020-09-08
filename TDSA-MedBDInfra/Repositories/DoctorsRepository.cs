using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDInfra.Repositories {
  public class DoctorsRepository
  : EntityBaseRepository<Doctor>, IDoctorsRepository {
    public DoctorsRepository(TDSAMedBDContext context) : base(context) {}

    public Doctor FindById(int id) {
      return DbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public Doctor FindByCpf(string cpf) {
      return DbSet.AsNoTracking().FirstOrDefault(x => x.Cpf == cpf);
    }

    public Doctor FindByCrm(string crm) {
      return DbSet.AsNoTracking().FirstOrDefault(x => x.Crm == crm);
    }

    public IEnumerable<Doctor> FindAll() {
      return DbSet.AsNoTracking()
        .Include(x => x.DoctorSpecialties)
        .ThenInclude(y => y.Specialty)
        .Where(x => x.DeletedAt == null)
        .ToList();
    }
  }
}
