using AutoMapper;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDAPI.Configurations {
  public class AutoMapperConfiguration : Profile {
    public AutoMapperConfiguration() {
      CreateMap<CreateDoctorViewModel, Doctor>().ForMember(
        x => x.Specialties, opt => opt.Ignore()
      );
    }
  }
}
