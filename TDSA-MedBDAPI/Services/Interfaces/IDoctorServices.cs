using TDSAMedBDAPI.ViewModels;

namespace TDSAMedBDAPI.Services.Interfaces {
  public interface IDoctorServices {
    public int RegisterDoctor(CreateDoctorViewModel doctor);
  }
}
