using System.Collections.Generic;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDDomain.Interfaces.Repositories {
  public interface IDoctorsRepository {
    void Add(Doctor doctor);
    void Save();
    public Doctor FindByCpf(string cpf);
    public Doctor FindByCrm(string crm);
    public IEnumerable<Doctor> FindAll();
  }
}
