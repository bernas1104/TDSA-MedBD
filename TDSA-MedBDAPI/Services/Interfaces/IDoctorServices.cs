using System.Collections.Generic;
using TDSA_MedBDAPI.ViewModels;

namespace TDSA_MedBDAPI.Services.Interfaces {
  public interface IDoctorServices {
    public IList<DoctorViewModel> ListDoctors();
    public string RegisterDoctor(CreateDoctorViewModel doctor);
  }
}
