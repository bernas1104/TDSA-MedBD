using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using TDSA_MedBDAPI.Exceptions;
using TDSA_MedBDAPI.Services;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDTest.Bogus.Models;
using TDSA_MedBDTest.Bogus.ViewModels;
using Xunit;

namespace TDSA_MedBDTest.UnitTests.Services {
  public class ListDoctorsBySpecialtyTest {
    private readonly Mock<IDoctorsRepository> doctorsRepository;
    private readonly Mock<ISpecialtiesRepository> specialtiesRepository;
    private readonly Mock<IDoctorSpecialtiesRepository> doctorSpecialtiesRepository;
    private readonly Mock<IMapper> mapper;
    private readonly IDoctorServices doctorServices;

    public ListDoctorsBySpecialtyTest() {
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
    public void Should_Return_List_Of_Doctor_ViewModels_Of_Specific_Specialty() {
      var rnd = new Random();
      var quantity = rnd.Next(2, 5);

      var specialtyName = "lorem";

      var specialty = SpecialtyFaker.Generate("lorem");
      var specialtyId = specialty.Id;

      var doctors = DoctorFaker.Generate(quantity);

      var doctorSpecialties = DoctorSpecialtyFaker.Generate(doctors, specialtyId);

      IList<DoctorViewModel> doctorViewModels = new List<DoctorViewModel>();
      foreach(var doctor in doctors)
        doctorViewModels.Add(DoctorViewModelFaker.Generate(doctor));

      specialtiesRepository.Setup(x => x.FindByName(specialtyName))
        .Returns(specialty);
      doctorSpecialtiesRepository.Setup(x => x.FindAllBySpecialtyId(specialtyId))
        .Returns(doctorSpecialties);

      for(var i = 0; i < doctorSpecialties.Count; i++) {
        doctorsRepository.Setup(x => x.FindById(doctorSpecialties[i].DoctorId))
          .Returns(doctors[i]);
      }

      mapper.Setup(x => x.Map<IList<DoctorViewModel>>(doctors))
        .Returns(doctorViewModels);

      var response = doctorServices.ListDoctorsBySpecialty(specialtyName);

      Assert.NotNull(response);
      Assert.Equal(quantity, response.Count);
    }

    [Fact]
    public void Should_Return_Empty_List_If_No_Doctors_Of_Specialty_Are_Found() {
      var specialtyName = "lorem";

      var specialty = SpecialtyFaker.Generate("lorem");
      var specialtyId = specialty.Id;

      specialtiesRepository.Setup(x => x.FindByName(specialtyName))
        .Returns(specialty);
      doctorSpecialtiesRepository.Setup(x => x.FindAllBySpecialtyId(specialtyId))
        .Returns(new List<DoctorSpecialty>());

      mapper.Setup(x => x.Map<IList<DoctorViewModel>>(new List<Doctor>()))
        .Returns(new List<DoctorViewModel>());

      var response = doctorServices.ListDoctorsBySpecialty(specialtyName);

      Assert.NotNull(response);
      Assert.Equal(0, response.Count);
    }

    [Fact]
    public void Should_Throw_Exception_If_Specialty_Not_Registered() {
      var specialtyName = "lorem";

      specialtiesRepository.Setup(x => x.FindByName(specialtyName))
        .Returns((Specialty)null);

      Assert.Throws<AppException>(
        () => doctorServices.ListDoctorsBySpecialty(specialtyName)
      );
    }
  }
}
