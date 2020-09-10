using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDInfra.Repositories {
  public class DoctorSpecialtiesRepository
  : EntityBaseRepository<DoctorSpecialty>, IDoctorSpecialtiesRepository {
    public DoctorSpecialtiesRepository(TDSAMedBDContext context) : base(context) {}

    public IList<DoctorSpecialty> FindAllBySpecialtyId(int specialtyId) {
      return DbSet.AsNoTracking()
        .Where(x => x.SpecialtyId == specialtyId)
        .ToList();
    }
  }
}
