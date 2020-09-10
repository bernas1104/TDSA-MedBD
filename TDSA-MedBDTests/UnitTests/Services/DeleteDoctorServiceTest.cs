using System;
using AutoMapper;
using Moq;
using TDSA_MedBDAPI.Exceptions;
using TDSA_MedBDAPI.Services;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDTest.Bogus.Models;
using Xunit;

namespace TDSA_MedBDTest.UnitTests.Services {
  public class DeleteDoctorServiceTest {
    private readonly Mock<IDoctorsRepository> doctorsRepository;
    private readonly Mock<ISpecialtiesRepository> specialtiesRepository;
    private readonly Mock<IDoctorSpecialtiesRepository> doctorSpecialtiesRepository;
    private readonly Mock<IMapper> mapper;
    private readonly IDoctorServices doctorServices;

    public DeleteDoctorServiceTest() {
      doctorsRepository = new Mock<IDoctorsRepository>();
      specialtiesRepository = new Mock<ISpecialtiesRepository>();
      doctorSpecialtiesRepository = new Mock<IDoctorSpecialtiesRepository>();
      mapper = new Mock<IMapper>();
      doctorServices = new DoctorServices(
        doctorsRepository.Object,
        specialtiesRepository.Object,
        doctorSpecialtiesRepository.Object,
        mapper.Object
      );
    }

    [Fact]
    public void Should_Set_Doctor_Delete_Date() {
      var rnd = new Random();
      var id = rnd.Next(1, 11);

      var doctor = DoctorFaker.Generate("84245777010", "1-DF");
      doctor.Id = id;

      doctorsRepository.Setup(x => x.FindById(id)).Returns(doctor);
      doctorsRepository.Setup(x => x.Update(doctor));
      doctorsRepository.Setup(x => x.Save());

      var result = doctorServices.DeleteDoctor(id);

      Assert.Equal(id, result);
    }

    [Fact]
    public void Should_Throw_Exception_If_Doctor_Not_Registered() {
      var rnd = new Random();
      var id = rnd.Next(1, 11);

      Assert.Throws<AppException>(() => doctorServices.DeleteDoctor(id));
    }

    [Fact]
    public void Should_Throw_Exception_If_Doctor_Already_Deleted() {
      var rnd = new Random();
      var id = rnd.Next(1, 11);

      var doctor = DoctorFaker.Generate("84245777010", "1-DF");
      doctor.DeletedAt = DateTime.Now;

      doctorsRepository.Setup(x => x.FindById(id)).Returns(doctor);

      Assert.Throws<AppException>(() => doctorServices.DeleteDoctor(id));
    }
  }
}
