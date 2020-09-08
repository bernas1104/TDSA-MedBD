using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDAPI.Configurations {
  public class AutoMapperConfiguration : Profile {
    public AutoMapperConfiguration() {
      CreateMap<CreateDoctorViewModel, Doctor>().ForMember(
        x => x.DoctorSpecialties, opt => opt.Ignore()
      );
      CreateMap<Doctor, DoctorViewModel>().ForMember(
        x => x.DoctorSpecialties, opt => opt.MapFrom(y => (
          y.DoctorSpecialties.Select(z => z.Specialty.Name).ToList()
        ))
      );
    }
  }
}
