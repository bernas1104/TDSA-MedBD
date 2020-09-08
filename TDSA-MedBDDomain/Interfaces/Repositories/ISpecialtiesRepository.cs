using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDDomain.Interfaces.Repositories {
  public interface ISpecialtiesRepository {
    public Specialty FindByName(string name);
  }
}
