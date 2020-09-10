using System.Collections.Generic;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDDomain.Interfaces.Repositories {
  public interface IDoctorSpecialtiesRepository {
    public IList<DoctorSpecialty> FindAllBySpecialtyId(int specialtyId);
  }
}
