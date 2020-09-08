using System.Collections.Generic;
using TDSA_MedBDAPI.ViewModels;

namespace TDSA_MedBDAPI.Services.Interfaces {
  public interface IDoctorServices {
    public IList<DoctorViewModel> ListDoctors();
    public int RegisterDoctor(CreateDoctorViewModel doctor);
    public int DeleteDoctor(int id);
  }
}
