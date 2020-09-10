using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDInfra.Repositories {
  public class SpecialtiesRepository
  : EntityBaseRepository<Specialty>, ISpecialtiesRepository {
    public SpecialtiesRepository(TDSAMedBDContext context) : base(context) {}

    public Specialty FindByName(string name) {
      return DbSet.AsNoTracking().FirstOrDefault(
        x => x.Name.ToLower() == name.ToLower()
      );
    }

    public IList<Specialty> FindAll() {
      return DbSet.AsNoTracking().ToList();
    }
  }
}
