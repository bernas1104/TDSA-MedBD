using System.Linq;
using AutoMapper;
using Moq;
using TDSA_MedBDAPI.Exceptions;
using TDSA_MedBDAPI.Services;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDTest.Bogus.Models;
using TDSA_MedBDTest.Bogus.ViewModels;
using Xunit;

namespace TDSA_MedBDTest.UnitTests.Services {
  public class DoctorServicesTest {
    private readonly Mock<IDoctorsRepository> doctorsRepository;
    private readonly Mock<ISpecialtiesRepository> specialtiesRepository;
    private readonly Mock<IMapper> mapper;
    private readonly IDoctorServices doctorServices;

    public DoctorServicesTest() {
      doctorsRepository = new Mock<IDoctorsRepository>();
      specialtiesRepository = new Mock<ISpecialtiesRepository>();
      mapper = new Mock<IMapper>();
      doctorServices = new DoctorServices(
        doctorsRepository.Object,
        specialtiesRepository.Object,
        mapper.Object
      );
    }

    [Fact]
    public void Should_Create_New_Doctor_And_Return_Its_Id() {
      var data = CreateDoctorViewModelFaker.Generate("1-DF", "06188740037");

      var model = DoctorFaker.Generate(data);

      var id = model.Id;

      mapper.Setup(x => x.Map<Doctor>(data)).Returns(model);

      doctorsRepository.Setup(x => x.FindByCpf(model.Cpf)).Returns((Doctor)null);
      doctorsRepository.Setup(x => x.FindByCrm(model.Crm)).Returns((Doctor)null);
      doctorsRepository.Setup(x => x.Add(model));

      foreach(var specialty in data.Specialties) {
        specialtiesRepository.Setup(x => x.FindByName(specialty))
          .Returns(SpecialtyFaker.Generate(specialty));
      }

      doctorsRepository.Setup(x => x.Save());

      var response = doctorServices.RegisterDoctor(data);

      Assert.Equal(id, response);
    }

    [Fact]
    public void Should_Throw_Exception_If_CPF_Not_Unique() {
      var data = CreateDoctorViewModelFaker.Generate("1-DF", "06188740037");

      var model = DoctorFaker.Generate(data);

      var id = model.Id;

      mapper.Setup(x => x.Map<Doctor>(data)).Returns(model);

      doctorsRepository.Setup(x => x.FindByCpf(model.Cpf)).Returns(new Doctor());

      Assert.Throws<AppException>(() => doctorServices.RegisterDoctor(data));
    }

    [Fact]
    public void Should_Throw_Exception_If_CRM_Not_Unique() {
      var data = CreateDoctorViewModelFaker.Generate("1-DF", "06188740037");

      var model = DoctorFaker.Generate(data);

      var id = model.Id;

      mapper.Setup(x => x.Map<Doctor>(data)).Returns(model);

      doctorsRepository.Setup(x => x.FindByCpf(model.Cpf)).Returns((Doctor)null);
      doctorsRepository.Setup(x => x.FindByCrm(model.Crm)).Returns(new Doctor());

      Assert.Throws<AppException>(() => doctorServices.RegisterDoctor(data));
    }

    [Fact]
    public void Should_Throw_Exception_If_Any_Specialty_Does_Not_Exist() {
      var data = CreateDoctorViewModelFaker.Generate("1-DF", "06188740037");

      var model = DoctorFaker.Generate(data);

      var id = model.Id;

      mapper.Setup(x => x.Map<Doctor>(data)).Returns(model);

      doctorsRepository.Setup(x => x.FindByCpf(model.Cpf)).Returns((Doctor)null);
      doctorsRepository.Setup(x => x.FindByCrm(model.Crm)).Returns((Doctor)null);

      specialtiesRepository.Setup(x => x.FindByName(data.Specialties.ToList()[0]))
        .Returns((Specialty)null);

      Assert.Throws<AppException>(() => doctorServices.RegisterDoctor(data));
    }
  }
}
