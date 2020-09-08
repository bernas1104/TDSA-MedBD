using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using TDSA_MedBDAPI.Services;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDTest.Bogus.Models;
using TDSA_MedBDTest.Bogus.ViewModels;
using Xunit;

namespace TDSA_MedBDTest.UnitTests.Services {
  public class ListAllDoctorsServiceTest {
    private readonly Mock<IDoctorsRepository> doctorsRepository;
    private readonly Mock<ISpecialtiesRepository> specialtiesRepository;
    private readonly Mock<IMapper> mapper;
    private readonly IDoctorServices doctorServices;

    public ListAllDoctorsServiceTest() {
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
    public void Should_Return_A_Collection_Of_Doctor_ViewModels() {
      var rnd = new Random();
      var rndNumber = rnd.Next(0, 3);

      IList<Doctor> doctors = new List<Doctor>();
      for(var i = 0; i < rndNumber; i++)
        doctors.Add(DoctorFaker.Generate("CPF", "CRM"));

      IList<DoctorViewModel> doctorViewModels = new List<DoctorViewModel>();
      for(var i = 0; i < doctors.Count; i++)
        doctorViewModels.Add(DoctorViewModelFaker.Generate(doctors[i]));

      doctorsRepository.Setup(x => x.FindAll()).Returns(doctors);
      mapper.Setup(x => x.Map<IList<DoctorViewModel>>(doctors))
        .Returns(doctorViewModels);

      var response = doctorServices.ListDoctors();

      Assert.NotNull(response);
      Assert.Equal(rndNumber, response.Count);
    }
  }
}
