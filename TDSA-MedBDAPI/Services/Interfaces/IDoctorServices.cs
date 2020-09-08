using TDSA_MedBDAPI.ViewModels;

namespace TDSA_MedBDAPI.Services.Interfaces {
  public interface IDoctorServices {
    public int RegisterDoctor(CreateDoctorViewModel doctor);
  }
}
